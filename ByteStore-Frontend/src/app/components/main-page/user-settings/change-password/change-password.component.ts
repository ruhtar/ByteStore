import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PasswordService } from 'src/app/services/user/password.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent {
  passwordForm!: FormGroup;
  password!: string;
  repassword!: string;
  showPassword: boolean = false;
  showRePassword: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private passwordService: PasswordService,
  ) {
    this.passwordForm = this.formBuilder.group({
      password: ['', Validators.required],
      repassword: ['', Validators.required],
    });
  }

  ngOnInit() {}

  public changePassword() {
    if (this.passwordForm.valid) {
      const newPassword = this.passwordForm.get('password')?.value;
      const confirmPassword = this.passwordForm.get('repassword')?.value;
      this.passwordService
        .changePassword(newPassword, confirmPassword)
        .subscribe();
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

  toggleRePassword() {
    this.showRePassword = !this.showRePassword;
    const passwordInput = document.getElementById(
      'repassword',
    ) as HTMLInputElement;
    if (passwordInput) {
      passwordInput.type = this.showRePassword ? 'text' : 'password';
    }
  }
}
