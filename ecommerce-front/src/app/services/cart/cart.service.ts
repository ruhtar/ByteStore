import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BuyOrderStatus } from 'src/app/enums/BuyOrderStatus';
import { ShoppingCartDto } from 'src/app/types/ShoppingCartDto';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private http: HttpClient) {}

  addToCart(productId: number): void {}

  buyOrder(userId: number) {
    return this.http.get<BuyOrderStatus>(
      API_PATH + `cart/buy?userId=${userId}`,
    );
  }

  getCartByUserId(userId: number) {
    return this.http.get<ShoppingCartDto>(API_PATH + `user/${userId}/cart`);
  }
}
