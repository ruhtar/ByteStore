import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { ProductService } from 'src/app/services/product/product.service';
import { TokenService } from 'src/app/services/token/token.service';
import { OrderItem } from 'src/app/types/OrderItem';
import { Product } from 'src/app/types/Product';
import { Review } from 'src/app/types/Review';
import Swal from 'sweetalert2';
import { ReviewComponent } from './review/review.component';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
})
export class ProductDetailComponent {
  quantityToAdd: number = 1;
  product = new Product();
  reviews: Review[] = [];
  userId!: number;
  logged!: boolean;
  productId!: number;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService,
    private tokenService: TokenService,
    private authService: AuthService,
    private dialogRef: MatDialog,
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

    this.userId = this.tokenService.getDecodedJwt().nameid;
    this.authService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });

    this.productService.getProductReviews(this.productId).subscribe((data) => {
      this.reviews = data;
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
    this.dialogRef.open(ReviewComponent, { data: this.productId });
  }
}
