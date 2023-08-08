import { Component } from '@angular/core';
import { IUser } from 'src/app/interfaces/IUser';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  user: IUser = {
    username: 'string',
    password: '!123Qwe'
  }

  constructor(private loginService: LoginService) {
  }

  login() {
    let result = this.loginService.signIn(this.user);
    console.log(result)
  }
}
