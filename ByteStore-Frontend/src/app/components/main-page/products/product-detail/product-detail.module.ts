import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { FooterModule } from 'src/app/components/footer/footer.module';
import { HeaderModule } from 'src/app/components/header/header.module';
import { ProductDetailComponent } from './product-detail.component';

const routes: Routes = [
  {
    path: '',
    component: ProductDetailComponent,
  },
];

@NgModule({
  declarations: [ProductDetailComponent],
  exports: [RouterModule],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HeaderModule,
    FooterModule,
    FormsModule,
  ],
})
export class ProductDetailModule {}
