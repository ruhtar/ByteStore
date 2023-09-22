import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from '../../footer/footer.module';
import { HeaderModule } from '../../header/header.module';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { PurchaseHistoryComponent } from './purchase-history/purchase-history.component';
import { UserSettingsComponent } from './user-settings.component';

const UserSettingsRoutes: Routes = [
  {
    path: '',
    component: UserSettingsComponent,
    children: [
      {
        path: 'info',
        loadChildren: () =>
          import('./info/info.module').then((u) => u.InfoModule),
      },
      {
        path: 'change-password',
        component: ChangePasswordComponent,
      },
      // {
      //   path: 'purchase-history',
      //   component: PurchaseHistoryComponent,
      // },
      // {
      //   path: '',
      //   component: UserSettingsComponent,
      // },
    ],
  },
];

@NgModule({
  declarations: [
    UserSettingsComponent,
    ChangePasswordComponent,
    PurchaseHistoryComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(UserSettingsRoutes),
    HeaderModule,
    FooterModule,
  ],
})
export class UserSettingsModule {}
