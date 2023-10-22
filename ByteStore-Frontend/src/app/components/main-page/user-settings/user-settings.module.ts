import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from '../../footer/footer.module';
import { HeaderModule } from '../../header/header.module';
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
  declarations: [UserSettingsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(UserSettingsRoutes),
    HeaderModule,
    FooterModule,
  ],
})
export class UserSettingsModule {}
