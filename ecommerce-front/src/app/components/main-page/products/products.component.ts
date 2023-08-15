import { Component } from '@angular/core';
import { IProduct } from 'src/app/interfaces/IProduct';
import { CartService } from 'src/app/services/cart/cart.service';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  products!: IProduct[]

constructor(private productService: ProductService, private cartService: CartService) {
}

  ngOnInit(){
    this.productService.getAllProducts().subscribe((productList: IProduct[])=>{
      this.products = productList;
    });
  }

  addToCart(productId: number): void{
    this.cartService.addToCart(productId);
  }
}
