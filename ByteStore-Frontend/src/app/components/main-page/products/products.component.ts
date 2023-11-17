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
  minPrice: number = 0;
  maxPrice: number = 0;
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
    this.filterByPrice();
    const filterLowerCase = this.filterValue.toLowerCase();
    console.log(filterLowerCase);

    this.exibidProducts = this.exibidProducts.filter((product) => {
      return product.name.toLowerCase().includes(filterLowerCase);
    });

    // Após filtrar por nome, também aplique o filtro de preço
    // this.filterByPrice();
  }

  filterByPrice() {
    this.exibidProducts = this.originalProducts; //To avoid filtering the same list over and over again
    this.sortProducts();
    if (this.minPrice > 0 || this.maxPrice > 0) {
      this.exibidProducts = this.exibidProducts.filter((product) => {
        if (this.minPrice > 0 && this.maxPrice > 0) {
          return (
            product.price >= this.minPrice && product.price <= this.maxPrice
          );
        } else if (this.minPrice > 0) {
          return product.price >= this.minPrice;
        } else if (this.maxPrice > 0) {
          return product.price <= this.maxPrice;
        }
        return true;
      });
    }
  }

  sortProducts() {
    if (this.selectedOption === 'name') this.orderByName(this.exibidProducts);
    if (this.selectedOption === 'priceAsc')
      this.sortByPriceAsc(this.exibidProducts);
    if (this.selectedOption === 'priceDesc')
      this.sortByPriceDesc(this.exibidProducts);
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

  sortByPriceAsc(list: Product[]) {
    list.sort((a, b) => {
      return a.price - b.price;
    });
  }

  sortByPriceDesc(list: Product[]) {
    list.sort((a, b) => {
      return b.price - a.price;
    });
  }
}
