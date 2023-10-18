import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { FormModule } from 'src/app/components/form/form.module';
import { ChangePasswordComponent } from './change-password.component';

const PasswordRoute: Routes = [
  {
    path: '',
    component: ChangePasswordComponent,
    outlet: 'settings',
  },
];

@NgModule({
  declarations: [ChangePasswordComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forChild(PasswordRoute),
    FormModule,
  ],
})
export class ChangePasswordModule {}
