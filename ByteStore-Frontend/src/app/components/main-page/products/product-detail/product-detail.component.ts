import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { ProductService } from 'src/app/services/product/product.service';
import { TokenService } from 'src/app/services/token/token.service';
import { UserService } from 'src/app/services/user/user.service';
import { OrderItem } from 'src/app/types/OrderItem';
import { Product } from 'src/app/types/Product';
import { Review } from 'src/app/types/Review';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  providers: [MessageService],
})
export class ProductDetailComponent {
  visible: boolean = false;
  quantityToAdd: number = 1;
  product = new Product();
  reviews: Review[] = [];
  userId!: number;
  logged!: boolean;
  productId!: number;
  selectedRating!: number;
  newCommentText!: string;
  id!: number;
  rating: any;
  value!: number;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService,
    private tokenService: TokenService,
    private authService: AuthService,
    private userService: UserService,
    private dialogRef: MatDialog,
    private messageService: MessageService,
  ) {}
  ngOnInit() {
    let paramMap = this.route.snapshot.paramMap.get('id');
    if (paramMap !== null) {
      this.productId = parseInt(paramMap);
    }

    this.productService
      .getProductById(this.productId)
      .subscribe((data: Product) => {
        this.product = data;
      });

    this.productService.getProductReviews(this.productId).subscribe((data) => {
      this.reviews = data;
    });
    this.userId = this.tokenService.getDecodedJwt().nameid;
    this.authService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });
  }

  addToCart(userId: number, product: Product, quantity: number) {
    const orderItem = new OrderItem();
    orderItem.productId = product.productId!;
    orderItem.Quantity = quantity;
    this.cartService.addToCart(userId, orderItem).subscribe(
      (response) => {
        if (response.status === 200)
          Swal.fire('Product added to shopping cart!', '', 'success');
      },
      (error) => {
        if (error.status === 400) {
          Swal.fire(
            `Invalid product quantity. Please, check how many products are available in stock.`,
            '',
            'error',
          );
        }
      },
      () => {},
    );
  }

  openModal() {
    this.userService.checkIfUserHasBoughtAProduct(this.productId).subscribe(
      (isAllowed) => {
        this.visible = true;
      },
      (error) => {
        this.messageService.add({
          severity: 'warn',
          summary: 'Warn',
          detail: 'You must have brought the product to review it.',
        });
      },
    );
  }

  createReview(comment: string) {
    this.visible = false;
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
