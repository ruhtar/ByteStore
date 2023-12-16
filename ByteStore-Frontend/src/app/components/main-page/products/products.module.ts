import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { RouterModule, Routes } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { SliderModule } from 'primeng/slider';
import { FooterModule } from 'src/app/shared/footer/footer.module';
import { HeaderModule } from '../../../shared/header/header.module';
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
    InputTextModule,
    InputNumberModule,
    SliderModule,
    ButtonModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    FormsModule,
    CommonModule,
    RouterModule.forChild(ProductRoute),
    HeaderModule,
    FooterModule,
  ],
})
export class ProductsModule {}
