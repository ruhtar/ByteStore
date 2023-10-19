import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from 'src/app/types/Product';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getAllProducts() {
    return this.http.get<Product[]>(API_PATH + 'products');
  }

  getProductById(productId: number) {
    return this.http.get<Product>(API_PATH + `products/${productId}`);
  }
}
