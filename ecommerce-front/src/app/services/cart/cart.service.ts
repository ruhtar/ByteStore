import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShoppingCart } from 'src/app/types/ShoppingCart';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private http: HttpClient) {}

  addToCart(productId: number): void {}

  getCartByUserId(userId: number) {
    return this.http.get<ShoppingCart>(API_PATH + `user/${userId}/cart`);
  }
}
