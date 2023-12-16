import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from 'src/app/shared/footer/footer.module';
import { HeaderModule } from '../../../shared/header/header.module';
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
        loadChildren: () =>
          import('./change-password/change-password.module').then(
            (u) => u.ChangePasswordModule,
          ),
      },
      {
        path: 'purchase-history',
        loadChildren: () =>
          import('./purchase-history/purchase-history.module').then(
            (u) => u.PurchaseHistoryModule,
          ),
      },
      // {
      //   path: '',
      //   component: UserSettingsComponent,
      // },
    ],
  },
];

@NgModule({
  declarations: [UserSettingsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(UserSettingsRoutes),
    HeaderModule,
    FooterModule,
  ],
})
export class UserSettingsModule {}
