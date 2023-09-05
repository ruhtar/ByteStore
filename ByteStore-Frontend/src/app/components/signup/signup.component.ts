import { Component } from '@angular/core';
import { Roles } from 'src/app/enums/Roles';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserAggregate } from 'src/app/types/UserAggregate';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
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

  constructor(private authService: AuthService) {}

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
    this.authService.signup(userAggregate).subscribe();
    this.authService.signIn(user).subscribe();
    window.location.replace('/home');
  }
}
