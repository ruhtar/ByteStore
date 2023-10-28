import { Component } from '@angular/core';
import { ProductService } from 'src/app/services/product/product.service';
import { Product } from 'src/app/types/Product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent {
  exibidProducts!: Product[];
  originalProducts!: Product[];
  filterValue: string = '';
  selectedOption: string = '';
  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.productService.getAllProducts().subscribe((productList: Product[]) => {
      this.exibidProducts = productList;
      this.originalProducts = productList;
    });
  }

  searchProducts() {
    const filterLowerCase = this.filterValue.toLowerCase();
    this.exibidProducts = this.originalProducts.filter((product) => {
      return product.name.toLowerCase().includes(filterLowerCase);
    });
  }

  onSelectChange() {
    if (this.selectedOption === 'name') this.orderByName(this.exibidProducts);
    if (this.selectedOption === 'priceAsc')
      this.orderByPriceAsc(this.exibidProducts);
    if (this.selectedOption === 'priceDesc')
      this.orderByPriceDesc(this.exibidProducts);
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

  orderByPriceAsc(list: Product[]) {
    list.sort((a, b) => {
      return a.price - b.price;
    });
  }

  orderByPriceDesc(list: Product[]) {
    list.sort((a, b) => {
      return b.price - a.price;
    });
  }
}
