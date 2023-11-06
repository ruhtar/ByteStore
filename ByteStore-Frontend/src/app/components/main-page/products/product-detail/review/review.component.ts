import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product/product.service';
import { TokenService } from 'src/app/services/token/token.service';
import { Review } from 'src/app/types/Review';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css'],
})
export class ReviewComponent {
  selectedRating!: number;
  rates: number[] = [0, 1, 2, 3, 4, 5];
  newCommentText!: string;
  id!: number;
  rating: any;
  constructor(
    private productService: ProductService,
    private tokenService: TokenService,
    @Inject(MAT_DIALOG_DATA) private productId: number,
  ) {}

  createReview(comment: string) {
    var username = this.tokenService.getDecodedJwt().name;
    var userId = this.tokenService.getDecodedJwt().nameid;
    const review = new Review();
    review.productId = this.productId;
    review.rate = this.selectedRating;
    review.userId = userId;
    review.reviewText = comment;
    review.username = username;

    this.productService.createReview(review).subscribe(
      (response) => {
        if (response.status === 200) {
          Swal.fire('Review posted.', '', 'success').then(() =>
            location.reload(),
          );
        }
      },
      (error) => {
        Swal.fire(
          'Whoops, something went wrong. Please, try again later.',
          '',
          'error',
        );
      },
    );
  }
}
