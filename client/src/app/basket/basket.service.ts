import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();
  shipping = 0;

  constructor(private http: HttpClient) {}

  createPaymentIntent() {
    const basket = this.getCurrentBasketValue();

    if (!basket || !basket.id) {
      console.error('❌ No basket found to create payment intent');
      return of(null); // تمنع crash لو السلة غير موجودة
    }

    return this.http.post<IBasket>(this.baseUrl + 'payments/' + basket.id, {})
      .pipe(
        map((updatedBasket: IBasket) => {
          this.basketSource.next(updatedBasket);
          this.calculateTotals();
          return updatedBasket;
        })
      );
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.shipping = deliveryMethod.cost;
    const basket = this.getCurrentBasketValue();
    basket.deliveryMethodId = deliveryMethod.id;
    basket.shippingPrice = deliveryMethod.cost;
    this.calculateTotals();
    this.setBasket(basket); // ترسل السلة للسيرفر أيضًا
  }

  getBasket(id: string) {
    const basketFromLocalStorage = localStorage.getItem('basket');
    if (basketFromLocalStorage) {
      const basket: IBasket = JSON.parse(basketFromLocalStorage);
      this.basketSource.next(basket);
      this.shipping = basket.shippingPrice;
      this.calculateTotals();
      return of(basket); // return observable
    }

    return this.http.get<IBasket>(this.baseUrl + 'baskets/' + id)
      .pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          this.shipping = basket.shippingPrice;
          this.calculateTotals();
          return basket;
        })
      );
  }

  setBasket(basket: IBasket) {
    localStorage.setItem('basket', JSON.stringify(basket));
    this.basketSource.next(basket);
    this.calculateTotals();

    // Send the basket to the backend
    this.http.post<IBasket>(this.baseUrl + 'baskets', basket).subscribe({
      next: () => console.log('✅ Basket synced with server'),
      error: err => console.error('❌ Error syncing basket:', err)
    });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    if (!item.id || !item.name) {
      console.warn('❌ محاولة إضافة منتج غير صالح إلى السلة:', item);
      return;
    }

    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const index = basket.items.findIndex(x => x.id === item.id);
    basket.items[index].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const index = basket.items.findIndex(x => x.id === item.id);
    if (basket.items[index].quantity > 1) {
      basket.items[index].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    if (basket.items.some(x => x.id === item.id)) {
      basket.items = basket.items.filter(i => i.id !== item.id);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteLocalBasket() {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket');
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'baskets/' + basket.id).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket');
      },
      error: err => console.log(err)
    });
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = this.shipping;
    const subtotal = basket.items.reduce((sum, item) => sum + item.price * item.quantity, 0);
    const total = subtotal + shipping;
    this.basketTotalSource.next({ shipping, subtotal, total });
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);
    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id ?? 999,
      Name: item.name ?? 'Temporary Name',
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType
    };
  }
}
