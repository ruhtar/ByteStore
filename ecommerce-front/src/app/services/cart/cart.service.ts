import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl: string = 'https://localhost:7010';
  constructor(private http: HttpClient) {}

  addToCart(productId: number): void {}
}
