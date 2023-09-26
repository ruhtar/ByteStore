import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from 'src/app/services/user/user.service';
import { Address } from 'src/app/types/Address';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css'],
})
export class InfoComponent {
  addressLabel: string = 'Address';
  streetLabel: string = 'Street';
  streetNumberLabel: string = 'Street Number';
  cityLabel: string = 'City';
  stateLabel: string = 'State';
  countryLabel: string = 'Country';
  submitButtonText: string = 'Edit';

  infoForm!: FormGroup;

  address!: Address;
  streetValue!: string;
  streetNumberValue!: number;
  countryValue!: string;
  stateValue!: string;
  cityValue!: string;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
  ) {}

  ngOnInit() {
    this.infoForm = this.formBuilder.group({
      street: [],
      streetNumber: [],
      city: [],
      state: [],
      country: [],
    });

    this.userService.getUserAddress().subscribe(async (response) => {
      this.address = response;
      console.log(this.address);

      this.streetValue = this.address.street;
      console.log(this.address.street);
      console.log(this.streetValue);

      this.streetNumberValue = this.address.streetNumber;
      this.countryValue = this.address.country;
      this.stateValue = this.address.state;
      this.cityValue = this.address.city;
    });
  }

  editInfos() {}
}
