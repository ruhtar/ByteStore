import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { RouterModule, Routes } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { FooterModule } from 'src/app/components/footer/footer.module';
import { HeaderModule } from 'src/app/components/header/header.module';
import { ProductDetailComponent } from './product-detail.component';
import { ReviewComponent } from './review/review.component';

const routes: Routes = [
  {
    path: '',
    component: ProductDetailComponent,
  },
];

@NgModule({
  declarations: [ProductDetailComponent, ReviewComponent],
  exports: [RouterModule],
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    CommonModule,
    RouterModule.forChild(routes),
    HeaderModule,
    FooterModule,
    FormsModule,
    SweetAlert2Module,
  ],
})
export class ProductDetailModule {}
