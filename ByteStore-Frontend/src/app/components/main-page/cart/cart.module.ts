import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ButtonModule } from 'primeng/button';
import { FooterModule } from '../../footer/footer.module';
import { HeaderModule } from '../../header/header.module';
import { CartComponent } from './cart.component';

const route: Routes = [{ path: '', component: CartComponent }];

@NgModule({
  declarations: [CartComponent],
  imports: [
    ButtonModule,
    HeaderModule,
    FooterModule,
    RouterModule.forChild(route),
    SweetAlert2Module,
    CommonModule,
  ],
})
export class CartModule {}
