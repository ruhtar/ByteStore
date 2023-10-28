import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
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
    SweetAlert2Module,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forChild(InfoRoute),
  ],
})
export class InfoModule {}
