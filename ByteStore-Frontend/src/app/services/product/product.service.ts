import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedDto } from 'src/app/types/PagedDto';
import { Product } from 'src/app/types/Product';
import { Review } from 'src/app/types/Review';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getAllProducts(pageIndex: number, pageSize: number) {
    return this.http.get<PagedDto<Product>>(
      API_PATH + `products?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    );
  }

  getProductReviews(productId: number) {
    return this.http.get<Review[]>(
      API_PATH + `products/reviews?productId=${productId}`,
    );
  }

  createReview(review: Review) {
    return this.http.post(API_PATH + `products/reviews`, review, {
      observe: 'response',
    });
  }

  getProductById(productId: number) {
    return this.http.get<Product>(API_PATH + `products/${productId}`);
  }
}
