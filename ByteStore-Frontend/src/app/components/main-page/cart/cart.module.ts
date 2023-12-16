import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { FooterModule } from 'src/app/shared/footer/footer.module';
import { HeaderModule } from '../../../shared/header/header.module';
import { CartComponent } from './cart.component';

const route: Routes = [{ path: '', component: CartComponent }];

@NgModule({
  declarations: [CartComponent],
  imports: [
    ToastModule,
    ButtonModule,
    HeaderModule,
    FooterModule,
    RouterModule.forChild(route),
    SweetAlert2Module,
    CommonModule,
  ],
})
export class CartModule {}
