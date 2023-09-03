import { Component } from '@angular/core';
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
  userId!: number;
  cart!: ShoppingCartDto;

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
          console.log(this.cart.products);
          // console.log(this.products);
        });
    }
  }
}
