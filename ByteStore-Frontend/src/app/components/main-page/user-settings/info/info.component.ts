import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css'],
})
export class InfoComponent {
  address: string = 'Address';
  street: string = 'Street';
  streetNumber: string = 'Street Number';
  city: string = 'City';
  state: string = 'State';
  country: string = 'Country';
  submitButtonText: string = 'Edit';
  infoForm!: FormGroup;

  ngOnInit() {}

  editInfos() {}
}
