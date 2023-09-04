import { Component } from '@angular/core';
import { BuyOrderStatus } from 'src/app/enums/BuyOrderStatus';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { TokenService } from 'src/app/services/token/token.service';
import { Product } from 'src/app/types/Product';
import { ShoppingCart } from 'src/app/types/ShoppingCart';
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
  products!: Product[];
  totalPrice: number = 0;

  ngOnInit() {
    this.authService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });

    if (this.logged) {
      this.userId = this.tokenService.getUserIdFromToken().nameid;
      this.cartService
        .getCartByUserId(this.userId)
        .subscribe((response: ShoppingCartDto) => {
          this.cart = response;
          this.products = response.products;
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
}
