import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product/product.service';
import { TokenService } from 'src/app/services/token/token.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css'],
})
export class ReviewComponent {
  constructor(
    private productService: ProductService,
    private tokenService: TokenService,
    @Inject(MAT_DIALOG_DATA) private productId: number,
  ) {}
}
