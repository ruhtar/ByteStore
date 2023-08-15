import { Component } from '@angular/core';
import { IProduct } from 'src/app/interfaces/IProduct';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  products!: IProduct[]

constructor(private productService: ProductService) {
}

  ngOnInit(){
    this.productService.getAllProducts().subscribe((productList: IProduct[])=>{
      this.products = productList;
    });
  }
}
