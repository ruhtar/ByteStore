import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/services/product/product.service';
import { Product } from 'src/app/types/Product';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
})
export class ProductDetailComponent {
  product: Product = {
    productQuantity: 0,
    name: '',
    price: 0,
  };

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
  ) {}
  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    if (id !== null) {
      this.productService
        .getProductById(parseInt(id))
        .subscribe((data: Product) => {
          this.product = data;
        });
    }
  }
}
