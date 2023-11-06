import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ReviewComponent } from './review.component';

@NgModule({
  declarations: [ReviewComponent],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatSelectModule,
    FormBuilder,
    FormGroup,
    FormControl,
    CommonModule,
  ],
})
export class ReviewModule {}
