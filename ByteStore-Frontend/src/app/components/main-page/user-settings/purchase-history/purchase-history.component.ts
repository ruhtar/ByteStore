import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-purchase-history',
  templateUrl: './purchase-history.component.html',
  styleUrls: ['./purchase-history.component.css'],
})
export class PurchaseHistoryComponent {
  sortField!: string;
  sortOrder!: number;
  searchText: string = '';
  filteredProducts!: any;

  products!: any;
  constructor(private userService: UserService) {}
  ngOnInit() {
    this.userService.getUserPurchaseHistory().subscribe((item) => {
      this.products = item;
    });
  }
  sortOptions = [
    { label: 'Name', value: 'Product.Name' },
    { label: 'Quantity (Asc)', value: 'Product.ProductQuantity' },
    { label: 'Quantity (Desc)', value: '!Product.ProductQuantity' },
    { label: 'Price (Asc)', value: 'Product.Price' },
    { label: 'Price (Desc)', value: '!Product.Price' },
    { label: 'Purchase Date (Asc)', value: 'PurchaseDate' },
    { label: 'Purchase Date (Desc)', value: '!PurchaseDate' },
  ];

  onSortChange(event: any) {
    let value = event.value;

    if (value.indexOf('!') === 0) {
      this.sortOrder = -1;
      this.sortField = value.substring(1, value.length);
    } else {
      this.sortOrder = 1;
      this.sortField = value;
    }
  }
}
