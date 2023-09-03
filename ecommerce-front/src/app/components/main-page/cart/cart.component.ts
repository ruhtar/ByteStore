import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { TokenService } from 'src/app/services/token/token.service';
import { ShoppingCart } from 'src/app/types/ShoppingCart';

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
  userId!: number;
  cart!: ShoppingCart;

  ngOnInit() {
    this.authService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });

    if (this.logged) {
      this.userId = this.tokenService.getUserIdFromToken().nameid;
      this.cartService
        .getCartByUserId(this.userId)
        .subscribe((cart: ShoppingCart) => {
          this.cart = cart;
        });
    }
    console.log(this.cart);
  }
}
