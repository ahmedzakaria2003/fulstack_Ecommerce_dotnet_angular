import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { IWishlist, IWishlistItem } from '../shared/models/Wishlist';
import { IProduct } from '../shared/models/product';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  baseUrl = environment.apiUrl + 'wishlist/';
  private wishlistSource = new BehaviorSubject<IWishlist>(null);
  wishlist$ = this.wishlistSource.asObservable();

  constructor(private http: HttpClient) {}

  getWishlist(id: string) {
    return this.http.get<IWishlist>(this.baseUrl + id).pipe(
      map((wishlist: IWishlist) => {
        this.wishlistSource.next(wishlist);
      })
    );
  }

  setWishlist(wishlist: IWishlist) {
    return this.http.post<IWishlist>(this.baseUrl, wishlist).subscribe({
      next: (response: IWishlist) => {
        this.wishlistSource.next(response);
      },
      error: err => console.log(err)
    });
  }

  getCurrentWishlistValue() {
    return this.wishlistSource.value;
  }

  addItemToWishlist(item: IProduct) {
    const itemToAdd: IWishlistItem = this.mapProductToWishlistItem(item);
    const wishlist = this.getCurrentWishlistValue() ?? this.createWishlist();

    if (!wishlist.items.find(x => x.id === itemToAdd.id)) {
      wishlist.items.push(itemToAdd);
      this.setWishlist(wishlist);
    }
  }

  removeItemFromWishlist(item: IWishlistItem) {
    const wishlist = this.getCurrentWishlistValue();
    wishlist.items = wishlist.items.filter(x => x.id !== item.id);
    if (wishlist.items.length === 0) {
      this.wishlistSource.next(null);
      localStorage.removeItem('wishlist_id');
    } else {
      this.setWishlist(wishlist);
    }
  }

  private createWishlist(): IWishlist {
    const wishlist: IWishlist = {
      id: this.generateId(),
      items: []
    };
    localStorage.setItem('wishlist_id', wishlist.id);
    return wishlist;
  }

  private generateId(): string {
    return localStorage.getItem('wishlist_id') ?? this.generateUUID();
  }

  private generateUUID(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      const r = Math.random() * 16 | 0;
      const v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  private mapProductToWishlistItem(product: IProduct): IWishlistItem {
    return {
      id: product.id,
      name: product.name,
      price: product.price,
      pictureUrl: product.pictureUrl,
      brand: product.productBrand,
      type: product.productType
    };
  }
}
