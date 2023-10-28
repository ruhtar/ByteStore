import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from '../../footer/footer.module';
import { HeaderModule } from '../../header/header.module';
import { ProductsComponent } from './products.component';

const ProductRoute: Routes = [
  {
    path: '',
    children: [
      { path: '', component: ProductsComponent },
      {
        path: ':id',
        loadChildren: () =>
          import('./product-detail/product-detail.module').then(
            (u) => u.ProductDetailModule,
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [ProductsComponent],
  imports: [
    FormsModule,
    CommonModule,
    MatSelectModule,
    RouterModule.forChild(ProductRoute),
    HeaderModule,
    FooterModule,
  ],
})
export class ProductsModule {}
