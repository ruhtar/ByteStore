import { Component } from '@angular/core';
import { Roles } from 'src/app/enums/Roles';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UserAggregate } from 'src/app/types/UserAggregate';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  formTitle: string = 'Sign up';
  username: string = 'Username';
  password: string = 'Password';
  repassword: string = 'Confirm your password';
  address: string = 'Address';
  street: string = 'Street';
  streetNumber: string = 'Street Number';
  city: string = 'City';
  state: string = 'State';
  country: string = 'Country';
  submitButtonText: string = 'Sign up';
  usernamePlaceholder: string = 'Your username';
  passwordPlaceholder: string = 'Your password';
  repasswordPlaceholder: string = 'Confirm your password';

  constructor(private authService: AuthService) {}

  handler(user: any) {
    let role = Roles.User; //For now, the system will not have Sellers.

    const userAggregate: UserAggregate = {
      user: {
        Username: user.username,
        Password: user.password,
      },
      role: role,
      address: {
        street: user.state,
        streetNumber: user.streetNumber,
        city: user.city,
        state: user.street,
        country: user.country,
      },
    };

    //TODO: Validar o signup, logar e redirecionar.
    this.authService.signup(userAggregate).subscribe(
      (response) => {
        if (response.status === 200) {
          Swal.fire(
            `Welcome, ${user.username}! Thanks for joining.`,
            '',
            'success',
          ).then(() => {
            this.authService.signIn(user).subscribe(() => {
              window.location.replace('/home');
            });
          });
        }
      },
      () => {
        Swal.fire(
          `Something went wrong. Please, try again later.`,
          '',
          'error',
        );
      },
    );
  }
}
