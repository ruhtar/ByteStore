import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { FormModule } from '../form/form.module';
import { SignupComponent } from './signup.component';

const SignupRoute: Routes = [
  {
    path: '',
    component: SignupComponent,
    // outlet: 'settings',
  },
];

@NgModule({
  declarations: [SignupComponent],
  imports: [
    SweetAlert2Module,
    CommonModule,
    RouterModule.forChild(SignupRoute),
    FormModule,
  ],
})
export class SignupModule {}
