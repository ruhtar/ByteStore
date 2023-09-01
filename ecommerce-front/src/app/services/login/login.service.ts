import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IToken } from 'src/app/interfaces/IToken';
import { IUser } from 'src/app/interfaces/IUser';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient) {}

  signIn(user: IUser) {
    return this.http.post<IToken>(API_PATH + 'login/signin', user);
  }
}
