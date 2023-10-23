import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PasswordService } from 'src/app/services/user/password.service';
import Swal from 'sweetalert2';

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
    const newPassword = this.passwordForm.get('password')?.value;
    const confirmPassword = this.passwordForm.get('repassword')?.value;
    if (newPassword !== confirmPassword) {
      Swal.fire('Please, write matching passwords.', '', 'error');
      return;
    }

    if (this.passwordForm.valid) {
      this.passwordService
        .changePassword(newPassword, confirmPassword)
        .subscribe(
          (response) => {
            if (response.status === 200) {
              Swal.fire('Success in changing password!', '', 'success');
            }
          },
          (error) => {
            if (error.status === 400)
              Swal.fire('Please, write matching passwords.', '', 'error');
          },
        );
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
