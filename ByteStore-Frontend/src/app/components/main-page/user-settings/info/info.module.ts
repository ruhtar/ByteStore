import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormModule } from '../../../form/form.module';
import { InfoComponent } from './info.component';

const InfoRoute: Routes = [
  {
    path: '',
    component: InfoComponent,
    outlet: 'settings',
  },
];

@NgModule({
  declarations: [InfoComponent],
  imports: [
    CommonModule,
    // ReactiveFormsModule,
    // FormsModule,
    RouterModule.forChild(InfoRoute),
    FormModule,
  ],
})
export class InfoModule {}
