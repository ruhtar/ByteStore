import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
  imports: [CommonModule, RouterModule.forChild(SignupRoute), FormModule],
})
export class SignupModule {}
