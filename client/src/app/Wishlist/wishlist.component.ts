import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IWishlist, IWishlistItem } from '../shared/models/Wishlist';
import { WishlistService } from './Wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html'
})
export class WishlistComponent implements OnInit {
  wishlist$: Observable<IWishlist>;

  constructor(private wishlistService: WishlistService) {}

  ngOnInit(): void {
    this.wishlist$ = this.wishlistService.wishlist$;
  }

  removeItem(item: IWishlistItem) {
    this.wishlistService.removeItemFromWishlist(item);
  }
}
