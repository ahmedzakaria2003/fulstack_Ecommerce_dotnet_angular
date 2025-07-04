import { IAddress } from "./address";

export interface IOrderToCreate {
  cartId: string; // ✅ صحح الاسم ليتوافق مع OrderDTO في الـ backend
  deliveryMethodId: number;
  shipToAddress: IAddress;
}


export interface IOrder {
    id: string;
    buyerEmail: string;
    orderDate: string;
    shipToAddress: IAddress;
    deliveryMethod: string;
    deliveryCost: number;
    items: IOrderItem[];
    subtotal: number;
    status: string;
    total: number;
  }
  
  export interface IOrderItem {
    productId: string;
    productName: string;
    pictureUrl: string;
    price: number;
    quantity: number;
  }