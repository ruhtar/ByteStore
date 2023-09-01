import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IToken } from 'src/app/interfaces/IToken';
import { User } from 'src/app/interfaces/User';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient) {}

  signIn(user: User) {
    return this.http.post<IToken>(API_PATH + 'login/signin', user);
  }
}
