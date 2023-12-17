import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { RouterModule, Routes } from '@angular/router';
import { DataViewModule } from 'primeng/dataview';
import { DropdownModule } from 'primeng/dropdown';
import { RatingModule } from 'primeng/rating';
import { PurchaseHistoryComponent } from './purchase-history.component';

const route: Routes = [
  {
    path: '',
    component: PurchaseHistoryComponent,
    outlet: 'settings',
  },
];

@NgModule({
  declarations: [PurchaseHistoryComponent],
  imports: [
    DropdownModule,
    RatingModule,
    DataViewModule,
    MatListModule,
    RouterModule.forChild(route),
    CommonModule,
  ],
})
export class PurchaseHistoryModule {}
