import { Component } from '@angular/core';
import { BuyOrderStatus } from 'src/app/enums/BuyOrderStatus';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { TokenService } from 'src/app/services/token/token.service';
import { OrderItem } from 'src/app/types/OrderItem';
import { Product } from 'src/app/types/Product';
import { ShoppingCartDto } from 'src/app/types/ShoppingCartDto';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent {
  constructor(
    private tokenService: TokenService,
    private cartService: CartService,
    public authService: AuthService,
  ) {}
  logged!: boolean;
  isCartEmpty: boolean = true;
  userId!: number;
  cart!: ShoppingCartDto;
  products: Product[] = [];
  totalPrice: number = 0;

  ngOnInit() {
    this.authService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });

    if (this.logged) {
      this.userId = this.tokenService.getJwtFromLocalStorage().nameid;
      this.cartService
        .getCartByUserId(this.userId)
        .subscribe((response: ShoppingCartDto) => {
          this.cart = response;
          this.products = response.products;
          this.products.forEach((product) => {
            console.log(product);
            console.log(product.productId);
          });
          if (this.products.length != 0) {
            this.isCartEmpty = false;
            this.calculateTotalPrice();
          }
        });
    }
  }

  buyOrder() {
    if (this.logged) {
      this.cartService
        .buyOrder(this.userId)
        .subscribe((response: BuyOrderStatus) => {
          if (response === BuyOrderStatus.Completed) {
            alert('Completed');
          }
        });
      location.reload();
    }
  }

  calculateTotalPrice() {
    this.cart.products.forEach((element) => {
      this.totalPrice += element.price * element.productQuantity;
    });
  }

  changeItemQuantity(productId: number, quantity: number) {
    console.log(productId);
    const orderItem: OrderItem = {
      productId: productId,
      Quantity: quantity,
    };
    this.cartService.addToCart(this.userId, orderItem);
  }
}
