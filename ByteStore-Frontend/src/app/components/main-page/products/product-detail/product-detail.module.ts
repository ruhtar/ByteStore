import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './product-detail.component';

const routes: Routes = [
  {
    path: '',
    component: ProductDetailComponent,
  },
];

@NgModule({
  declarations: [],
  exports: [RouterModule],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class ProductDetailModule {}
