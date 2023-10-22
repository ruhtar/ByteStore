import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { User } from 'src/app/types/User';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm!: FormGroup;
  showPassword: boolean = false;
  invalidAuth: boolean = false;
  systemError: boolean = false;

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

    this.onChanges();
  }
  onChanges() {
    this.loginForm.valueChanges.subscribe(() => {
      this.systemError = false;
      this.invalidAuth = false;
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
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
      this.invalidAuth = true;
    }
    if (error.status == 500) {
      this.systemError = true;
    }
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
    const passwordInput = document.getElementById(
      'password',
    ) as HTMLInputElement;
    if (passwordInput) {
      passwordInput.type = this.showPassword ? 'text' : 'password';
    }
  }
}
