import { Component } from '@angular/core';
import { ProductService } from 'src/app/services/product/product.service';
import { Product } from 'src/app/types/Product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent {
  products!: Product[];
  filteredProducts!: Product[];
  originalProducts!: Product[];
  filterValue: string = '';
  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.productService.getAllProducts().subscribe((productList: Product[]) => {
      this.products = productList;
      this.originalProducts = productList;
    });
  }

  filterProducts() {
    if (this.filterValue === '') {
      this.products = this.originalProducts;
      return;
    }

    const filterLowerCase = this.filterValue.toLowerCase();
    this.products = this.originalProducts.filter((product) => {
      return product.name.toLowerCase().includes(filterLowerCase);
    });
  }
}
