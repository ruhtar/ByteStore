import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BuyOrderStatus } from 'src/app/enums/BuyOrderStatus';
import { OrderStatus } from 'src/app/enums/OrderStatus';
import { OrderItem } from 'src/app/types/OrderItem';
import { Product } from 'src/app/types/Product';
import { ShoppingCartDto } from 'src/app/types/ShoppingCartDto';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private http: HttpClient) {}

  addToCart(userId: number, orderItem: OrderItem) {
    return this.http
      .post<OrderStatus>(API_PATH + `cart/order?userId=${userId}`, orderItem)
      .subscribe();
  }

  buyOrder(userId: number) {
    return this.http.get<BuyOrderStatus>(
      API_PATH + `cart/buy?userId=${userId}`,
    );
  }

  getCartByUserId(userId: number) {
    return this.http.get<ShoppingCartDto>(API_PATH + `user/${userId}/cart`);
  }
}
