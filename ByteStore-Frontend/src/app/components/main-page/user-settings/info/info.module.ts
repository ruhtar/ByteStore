import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
  imports: [CommonModule, RouterModule.forChild(InfoRoute)],
})
export class InfoModule {}
