import { Component } from '@angular/core';
import { ProductService } from 'src/app/services/product/product.service';
import { PagedDto } from 'src/app/types/PagedDto';
import { Product } from 'src/app/types/Product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent {
  exibidProducts!: Product[];
  originalProducts!: Product[];
  minPrice: number = 0;
  maxPrice: number = 0;
  filterValue: string = '';
  selectedOption: string = '';
  pageIndex = 0;
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 15, 20];
  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.getPaginatedProducst(this.pageIndex, this.pageSize);
  }

  getPaginatedProducst(pageIndex: number, pageSize: number) {
    this.productService
      .getAllProducts(pageIndex, pageSize)
      .subscribe((pagedDto: PagedDto<Product>) => {
        this.exibidProducts = pagedDto.items;
        this.originalProducts = pagedDto.items;
      });
  }

  searchProducts() {
    this.filterByPrice();
    const filterLowerCase = this.filterValue.toLowerCase();

    this.exibidProducts = this.exibidProducts.filter((product) => {
      return product.name.toLowerCase().includes(filterLowerCase);
    });

    // Ap�s filtrar por nome, tamb�m aplique o filtro de pre�o
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

  pageChanged(event: any): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    // Chame aqui a fun��o que carrega os produtos com base na nova p�gina
    // Por exemplo: this.loadProducts();
    this.getPaginatedProducst(this.pageIndex, this.pageSize);
  }
}
