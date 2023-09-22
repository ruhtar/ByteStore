import { Component } from '@angular/core';
import { Roles } from 'src/app/enums/Roles';
import { UserAggregate } from 'src/app/types/UserAggregate';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css'],
})
export class InfoComponent {
  formTitle: string = 'Sign up';
  username: string = 'Username';
  password: string = 'Password';
  rePassword: string = 'Confirm your password';
  address: string = 'Address';
  street: string = 'Street';
  streetNumber: string = 'Street Number';
  city: string = 'City';
  state: string = 'State';
  country: string = 'Country';
  submitButtonText: string = 'Sign up';
  usernamePlaceholder: string = 'Your username';
  passwordPlaceholder: string = 'Your password';

  ngOnInit() {}

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

    //TODO: Validar o signup, logar e redirecionar.

    window.location.replace('/home');
  }
}
