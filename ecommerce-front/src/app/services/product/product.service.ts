import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProduct } from 'src/app/interfaces/IProduct';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  baseUrl: string = 'https://localhost:7010';
  constructor(private http: HttpClient) {}

  getAllProducts() {
    return this.http.get<IProduct[]>(this.baseUrl + '/products');
  }
}
