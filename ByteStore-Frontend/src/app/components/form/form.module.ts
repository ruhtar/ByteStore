import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormComponent } from './form.component';

@NgModule({
  declarations: [FormComponent],
  exports: [FormComponent],
  imports: [CommonModule, ReactiveFormsModule],
})
export class FormModule {}
