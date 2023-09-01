import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserAggregate } from 'src/app/interfaces/UserAggregate';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
})
export class FormComponent {
  @Input() formTitle!: string;
  @Input() username!: string;
  @Input() password!: string;
  @Input() address!: string;
  @Input() street!: string;
  @Input() streetNumber!: string;
  @Input() city!: string;
  @Input() state!: string;
  @Input() country!: string;
  @Input() submitButtonText!: string;
  @Input() usernamePlaceholder!: string;
  @Input() passwordPlaceholder!: string;

  userForm!: FormGroup;
  userAggregate!: UserAggregate;
  formSucess: boolean = false;
  @Output() onSubmit = new EventEmitter<UserAggregate>(); //Saída. Enviar dados para o componente pai

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.userForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      isSeller: [''],
      street: ['', Validators.required],
      streetNumber: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
    });
  }

  submitForm() {
    if (this.userForm.invalid) return;
    this.onSubmit.emit(this.userForm.value);
    this.userForm.reset();
    this.formSucess = true;
  }
}
