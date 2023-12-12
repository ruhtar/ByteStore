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
  range: number[] = [150, 2000];
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
    const filterLowerCase = this.filterValue.toLowerCase();

    this.exibidProducts = this.exibidProducts.filter((product) => {
      return product.name.toLowerCase().includes(filterLowerCase);
    });

    // Ap�s filtrar por nome, tamb�m aplique o filtro de pre�o
    // this.filterByPrice();
  }

  applyFilters() {
    this.exibidProducts = [...this.originalProducts];
    this.searchProducts();
    this.filterByPrice();
    // this.sortProducts();
  }

  filterByPrice() {
    if (this.range[0] > 0 || this.range[1] > 0) {
      this.exibidProducts = this.exibidProducts.filter((product) => {
        return (
          (this.range[0] <= 0 || product.price >= this.range[0]) &&
          (this.range[1] <= 0 || product.price <= this.range[1])
        );
      });
    }
  }

  sortProducts() {
    if (this.selectedOption === 'name') {
      this.orderByName(this.exibidProducts);
    } else if (this.selectedOption === 'priceAsc') {
      this.sortByPriceAsc(this.exibidProducts);
    } else if (this.selectedOption === 'priceDesc') {
      this.sortByPriceDesc(this.exibidProducts);
    }
  }

  orderByName(list: Product[]) {
    list.sort((a, b) => a.name.localeCompare(b.name));
  }

  sortByPriceAsc(list: Product[]) {
    list.sort((a, b) => a.price - b.price);
  }

  sortByPriceDesc(list: Product[]) {
    list.sort((a, b) => b.price - a.price);
  }

  pageChanged(event: any): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    // Chame aqui a fun��o que carrega os produtos com base na nova p�gina
    // Por exemplo: this.loadProducts();
    this.getPaginatedProducst(this.pageIndex, this.pageSize);
  }
}
