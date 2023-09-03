import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShoppingCartDto } from 'src/app/types/ShoppingCartDto';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private http: HttpClient) {}

  addToCart(productId: number): void {}

  getCartByUserId(userId: number) {
    return this.http.get<ShoppingCartDto>(API_PATH + `user/${userId}/cart`);
  }
}
