import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { InfoComponent } from './info/info.component';
import { PurchaseHistoryComponent } from './purchase-history/purchase-history.component';

const UserSettingsRoutes: Routes = [
  {
    path: '',
    children: [
      { path: 'info', component: InfoComponent },
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'purchase-history', component: PurchaseHistoryComponent },
    ],
  },
];

@NgModule({
  declarations: [
    InfoComponent,
    ChangePasswordComponent,
    PurchaseHistoryComponent,
  ],
  imports: [CommonModule, RouterModule.forChild(UserSettingsRoutes)],
})
export class UserSettingsModule {}
