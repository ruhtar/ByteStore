import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUser } from 'src/app/interfaces/IUser';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm!: FormGroup;
  token!: any

  constructor(private loginService: LoginService, private formBuilder: FormBuilder) {
  }
  ngOnInit(){
    this.loginForm = this.formBuilder.group({
      username: ["", Validators.required],
      password: ["", Validators.required]
    })
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const username: string = this.loginForm.get("username")?.value;
      const password: string = this.loginForm.get("password")?.value;
      const user: IUser = {
        username: username,
        password: password
      }

        this.loginService.signIn(user).subscribe(item=>
          {this.token = item}, (error: HttpErrorResponse) => {
            this.handleError(error)
          }
        );
      
    }
  }

  handleError(error: HttpErrorResponse){
    if(error.status == 401){
      alert("Incorrect username or password.")
    }
  }
}
