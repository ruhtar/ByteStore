import { Component } from '@angular/core';
import { Roles } from 'src/app/enums/Roles';
import { Address } from 'src/app/types/Address';
import { User } from 'src/app/types/User';
import { UserAggregate } from 'src/app/types/UserAggregate';
import { SignupService } from 'src/app/services/signup/signup.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  formTitle: string = 'Sign up';
  username: string = 'Username';
  password: string = 'Password';
  address: string = 'Address';
  street: string = 'Street';
  streetNumber: string = 'Street Number';
  city: string = 'City';
  state: string = 'State';
  country: string = 'Country';
  submitButtonText: string = 'Sign up';
  usernamePlaceholder: string = 'Your username';
  passwordPlaceholder: string = 'Your password';

  constructor(private signupService: SignupService) {}

  handler(user: any) {
    let role;

    var checkbox = <HTMLInputElement>document.getElementById('isSeller');
    if (checkbox.checked) {
      role = Roles.Seller;
    } else {
      role = Roles.User;
    }

    const userAggregate: UserAggregate = {
      user: {
        Username: user.username,
        Password: user.password,
      },
      role: role,
      address: {
        Street: user.state,
        StreetNumber: user.streetNumber,
        City: user.city,
        State: user.street,
        Country: user.country,
      },
    };

    this.signupService.signup(userAggregate).subscribe();
  }
}
