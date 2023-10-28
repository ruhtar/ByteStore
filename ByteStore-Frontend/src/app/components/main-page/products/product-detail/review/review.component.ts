import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product/product.service';
import { TokenService } from 'src/app/services/token/token.service';
import { Review } from 'src/app/types/Review';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css'],
})
export class ReviewComponent {
  newCommentText!: string;
  id!: number;
  constructor(
    private productService: ProductService,
    private tokenService: TokenService,
    @Inject(MAT_DIALOG_DATA) private productId: number,
  ) {}

  postComment(comment: string) {
    var username = this.tokenService.getDecodedJwt().name;
    const review = new Review();
    review.productId = this.productId;
    review.reviewText = comment;
    review.username = username;

    this.productService.createReview(review).subscribe();
  }
}
