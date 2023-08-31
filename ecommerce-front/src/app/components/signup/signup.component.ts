import { Component } from '@angular/core';
import { IUserAggregate } from 'src/app/interfaces/IUserAggregate';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {
  formTitle: string = 'Sign up';

  handler(user: IUserAggregate) {}
}
