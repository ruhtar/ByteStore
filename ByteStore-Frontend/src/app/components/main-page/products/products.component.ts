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
  selectedOption: string = '';
  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.productService.getAllProducts().subscribe((productList: Product[]) => {
      this.products = productList;
      this.originalProducts = productList;
    });
  }

  searchProducts() {
    if (this.filterValue === '') {
      this.products = this.originalProducts;
      return;
    }

    const filterLowerCase = this.filterValue.toLowerCase();
    this.products = this.originalProducts.filter((product) => {
      return product.name.toLowerCase().includes(filterLowerCase);
    });
  }

  onSelectChange() {
    if (this.selectedOption === 'name') this.orderByName(this.products);
    if (this.selectedOption === 'price') this.orderByPrice(this.products);
  }

  orderByName(list: Product[]) {
    list.sort((a, b) => {
      const nomeA = a.name.toLowerCase();
      const nomeB = b.name.toLowerCase();

      if (nomeA < nomeB) return -1;
      if (nomeA > nomeB) return 1;
      return 0;
    });
  }

  orderByPrice(list: Product[]) {
    list.sort((a, b) => {
      return a.price - b.price;
    });
  }
}
