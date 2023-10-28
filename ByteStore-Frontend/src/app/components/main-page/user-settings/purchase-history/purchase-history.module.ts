import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { RouterModule, Routes } from '@angular/router';
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
  imports: [MatListModule, RouterModule.forChild(route), CommonModule],
})
export class PurchaseHistoryModule {}
