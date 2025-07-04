import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { WishlistService } from 'src/app/Wishlist/Wishlist.service'; // ✅ أضف السطر ده

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct;

  constructor(
    private basketService: BasketService,
    private wishlistService: WishlistService
  ) {}

  ngOnInit(): void {}

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product);
  }

  addToWishlist() {
    this.wishlistService.addItemToWishlist(this.product);
  }
}
