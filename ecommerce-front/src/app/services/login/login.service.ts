import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IToken } from 'src/app/interfaces/IToken';
import { IUser } from 'src/app/interfaces/IUser';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  baseUrl: string = 'https://localhost:7010';
  constructor(private http: HttpClient) {}

  signIn(user: IUser) {
    return this.http.post<IToken>(this.baseUrl + '/login/signin', user);
  }
}
