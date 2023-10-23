import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderItem } from 'src/app/types/OrderItem';
import { ShoppingCartDto } from 'src/app/types/ShoppingCartDto';
import { API_PATH } from 'src/environment/env';
import { TokenService } from '../token/token.service';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) {}

  addToCart(userId: number, orderItem: OrderItem) {
    return this.http.post(API_PATH + `cart/order?userId=${userId}`, orderItem, {
      headers: this.tokenService.addJwtToHeaders(),
      observe: 'response',
      responseType: 'text',
    });
  }

  buyOrder(userId: number) {
    return this.http.get(API_PATH + `cart/buy?userId=${userId}`, {
      headers: this.tokenService.addJwtToHeaders(),
      observe: 'response',
      responseType: 'text',
    });
  }

  getCartByUserId(userId: number) {
    return this.http.get<ShoppingCartDto>(API_PATH + `user/${userId}/cart`);
  }
}
