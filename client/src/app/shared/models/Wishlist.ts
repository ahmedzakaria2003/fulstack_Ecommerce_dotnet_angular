export interface IWishlist {
  id: string;
  items: IWishlistItem[];
}

export interface IWishlistItem {
  id: number;
  name: string;
  price: number;
  pictureUrl: string;
  brand: string;
  type: string;
}
