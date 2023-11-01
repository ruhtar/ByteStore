import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from 'src/app/types/Product';
import { Review } from 'src/app/types/Review';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getAllProducts() {
    return this.http.get<Product[]>(API_PATH + 'products');
  }

  getProductReviews(productId: number) {
    return this.http.get<Review[]>(
      API_PATH + `products/review?productId=${productId}`,
    );
  }

  createReview(review: Review) {
    return this.http.post(API_PATH + `products/review`, review, {
      observe: 'response',
    });
  }

  getProductById(productId: number) {
    return this.http.get<Product>(API_PATH + `products/${productId}`);
  }
}
