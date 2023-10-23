import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { TokenService } from 'src/app/services/token/token.service';
import { OrderItem } from 'src/app/types/OrderItem';
import { Product } from 'src/app/types/Product';
import { ShoppingCartDto } from 'src/app/types/ShoppingCartDto';
import Swal from 'sweetalert2';

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
      this.userId = this.tokenService.getDecodedJwt().nameid;
      this.cartService
        .getCartByUserId(this.userId)
        .subscribe((response: ShoppingCartDto) => {
          this.cart = response;
          this.products = response.products;
          this.products.forEach((product) => {});
          if (this.products.length != 0) {
            this.isCartEmpty = false;
            this.calculateTotalPrice();
          }
        });
    }
  }

  buyOrder() {
    if (this.logged) {
      Swal.fire({
        title:
          'Are you sure to complete the purchase and buy all the products in your shopping cart?',
        confirmButtonText: 'Yes, complete the purchase.',
        icon: 'warning',
        showDenyButton: true,
        confirmButtonColor: '#28a028',
      }).then((result) => {
        if (result.isConfirmed) {
          this.cartService.buyOrder(this.userId).subscribe((response) => {
            if (response.status === 200) {
              Swal.fire('Thanks for your purchase!', '', 'success').then(() => {
                location.reload();
              });
            } else {
              Swal.fire('Whoops, some error occurred.', '', 'error');
              return;
            }
          });
        }
        return;
      });
    }
  }

  calculateTotalPrice() {
    this.totalPrice = 0;
    this.cart.products.forEach((element) => {
      this.totalPrice += element.price * element.productQuantity;
    });
  }

  changeItemQuantity(product: Product, quantity: number) {
    if (product.productQuantity === 1 && quantity < 0) {
      return;
    }
    const orderItem: OrderItem = {
      productId: product.productId,
      Quantity: quantity,
    };
    this.cartService.addToCart(this.userId, orderItem).subscribe((response) => {
      if (response.status === 200) {
        product.productQuantity += quantity;
        this.calculateTotalPrice();
      }
    });
  }
}
