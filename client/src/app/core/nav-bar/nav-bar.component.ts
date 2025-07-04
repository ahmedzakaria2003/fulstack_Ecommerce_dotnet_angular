import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { WishlistService } from 'src/app/Wishlist/Wishlist.service'; 
import { IBasket } from 'src/app/shared/models/basket';
import { IUser } from 'src/app/shared/models/user';
import { IWishlist } from 'src/app/shared/models/Wishlist';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  wishlist$: Observable<IWishlist>; 
  currentUser$: Observable<IUser>;

  constructor(
    private basketService: BasketService,
    private wishlistService: WishlistService, 
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;

    const wishlistId = localStorage.getItem('wishlist_id');
    if (wishlistId) {
      this.wishlistService.getWishlist(wishlistId).subscribe();
    }

    this.wishlist$ = this.wishlistService.wishlist$;

    

  }

  logout() {
    this.accountService.logout();
  }
}
