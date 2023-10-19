import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from '../footer/footer.module';
import { HeaderModule } from '../header/header.module';
import { CartComponent } from './cart/cart.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { MainPageComponent } from './main-page.component';

const MainPageRoutes: Routes = [
  {
    path: '',
    children: [
      { path: 'home', component: HomeComponent },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      {
        path: 'products',
        loadChildren: () =>
          import('./products/products.module').then((u) => u.ProductsModule),
      },
      { path: 'cart', component: CartComponent },
      { path: 'contact', component: ContactComponent },
      {
        path: 'settings',
        loadChildren: () =>
          import('./user-settings/user-settings.module').then(
            (u) => u.UserSettingsModule,
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [
    MainPageComponent,
    //ProductDetailComponent,
    //UserSettingsComponent,
  ],
  imports: [
    CommonModule,
    HeaderModule,
    FooterModule,
    RouterModule.forChild(MainPageRoutes),
  ],
})
export class MainPageModule {}
