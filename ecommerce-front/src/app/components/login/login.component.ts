import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IToken } from 'src/app/interfaces/IToken';
import { User } from 'src/app/interfaces/User';
import { UserAggregate } from 'src/app/interfaces/UserAggregate';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm!: FormGroup;

  constructor(
    private loginService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
  ) {}
  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    //this.loginForm.valid
    if (true) {
      const username: string = this.loginForm.get('username')?.value;
      const password: string = this.loginForm.get('password')?.value;
      const user: User = {
        Username: username,
        Password: password,
      };
      this.loginService.signIn(user).subscribe(
        (response) => {
          this.router.navigateByUrl('/home');
        },
        (error: HttpErrorResponse) => {
          this.handleError(error);
        },
      );
    }
  }

  handleError(error: HttpErrorResponse) {
    if (error.status == 401) {
      alert('Incorrect username or password.');
    }
    if (error.status == 500) {
      alert('We are facing some problems. Please, try again later.');
    }
  }
}
