import { Component } from '@angular/core';
import { Roles } from 'src/app/enums/Roles';
import { IUserAggregate } from 'src/app/interfaces/IUserAggregate';
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

  newUser!: IUserAggregate;

  constructor(private signupService: SignupService) {}

  handler(user: IUserAggregate) {
    console.log(user);
    this.newUser.address.Country = user.address.Country;
    this.newUser.address.City = user.address.City;
    this.newUser.user.Password = user.user.Password;
    this.newUser.user.Username = user.user.Username;
    this.newUser.address.State = user.address.State;
    this.newUser.address.Street = user.address.Street;
    this.newUser.address.StreetNumber = user.address.StreetNumber;

    var checkbox = <HTMLInputElement>document.getElementById('isSeller');
    if (checkbox.checked) {
      this.newUser.role = Roles.Seller;
    } else {
      this.newUser.role = Roles.User;
    }

    this.signupService.signup(user).subscribe();
  }
}
