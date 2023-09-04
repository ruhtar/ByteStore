import { Component } from '@angular/core';
import { Product } from 'src/app/types/Product';
import { CartService } from 'src/app/services/cart/cart.service';
import { ProductService } from 'src/app/services/product/product.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { TokenService } from 'src/app/services/token/token.service';
import { OrderItem } from 'src/app/types/OrderItem';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent {
  products!: Product[];
  selectedQuantity: number = 0;
  userId!: number;
  logged!: boolean;

  constructor(
    private productService: ProductService,
    private tokenService: TokenService,
    private cartService: CartService,
    private authService: AuthService,
  ) {}

  ngOnInit() {
    this.productService.getAllProducts().subscribe((productList: Product[]) => {
      this.products = productList;
    });

    this.authService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });

    if (this.logged) {
      this.userId = this.tokenService.getUserIdFromToken().nameid;
    }
    console.log(this.userId);
  }

  addToCart(userId: number, product: Product): void {
    const orderItem = new OrderItem();
    orderItem.ProductId = product.productId!;
    orderItem.Quantity = 1;
    this.cartService.addToCart(this.userId, orderItem);
  }
}
