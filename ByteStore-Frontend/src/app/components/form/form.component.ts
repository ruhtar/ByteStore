import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PasswordService } from 'src/app/services/user/password.service';
import { UserAggregate } from 'src/app/types/UserAggregate';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
})
export class FormComponent {
  @Input() formTitle!: string;
  @Input() username!: string;
  @Input() password!: string;
  @Input() repassword!: string;
  @Input() address!: string;
  @Input() street!: string;
  @Input() streetNumber!: string;
  @Input() city!: string;
  @Input() state!: string;
  @Input() country!: string;
  @Input() submitButtonText!: string;
  @Input() usernamePlaceholder!: string;
  @Input() passwordPlaceholder!: string;
  @Input() repasswordPlaceholder!: string;
  showPassword: boolean = false;
  showRepassword: boolean = false;

  userForm!: FormGroup;
  userAggregate!: UserAggregate;
  formSucess: boolean = false;
  @Output() onSubmit = new EventEmitter<UserAggregate>(); //Saída. Enviar dados para o componente pai

  constructor(
    private formBuilder: FormBuilder,
    private passwordService: PasswordService,
  ) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.userForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      repassword: ['', Validators.required],
      street: ['', Validators.required],
      streetNumber: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
    });
  }

  submitForm() {
    if (this.userForm.invalid) return;
    if (
      this.userForm.get('repassword')!.value !==
        this.userForm.get('password')!.value &&
      this.userForm.valid
    ) {
      Swal.fire('Please, use matching passwords.', '', 'error');
      return;
    }
    //TODO: ADICIONAR VALIDAÇÃO DE SENHA NO FRONT
    if (
      !this.passwordService.isPasswordValid(
        this.userForm.get('password')!.value,
      ) &&
      this.userForm.valid
    ) {
      Swal.fire(
        'Your password must have capital letters, numbers and special characters',
        '',
        'error',
      );
      return;
    }
    this.onSubmit.emit(this.userForm.value);
    this.formSucess = true;
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

  toggleRepassword() {
    this.showRepassword = !this.showRepassword;
    const passwordInput = document.getElementById(
      'repassword',
    ) as HTMLInputElement;
    if (passwordInput) {
      passwordInput.type = this.showRepassword ? 'text' : 'password';
    }
  }
}
