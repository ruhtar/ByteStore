import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-purchase-history',
  templateUrl: './purchase-history.component.html',
  styleUrls: ['./purchase-history.component.css'],
})
export class PurchaseHistoryComponent {
  products!: any;
  constructor(private userService: UserService) {}
  ngOnInit() {
    this.userService.getUserPurchaseHistory().subscribe((item) => {
      this.products = item;
    });
  }
}
