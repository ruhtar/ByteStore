import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IToken } from 'src/app/interfaces/IToken';
import { User } from 'src/app/interfaces/User';
import { API_PATH } from 'src/environment/env';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _isLoggedIn = new BehaviorSubject<boolean>(false);
  isLoggedIn = this._isLoggedIn.asObservable();

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('acess_token');
    //without this, if we reload the page, it counts as if we are not logged in
    this._isLoggedIn.next(!!token); //transforms the token value in a boolean
  }

  signIn(user: User) {
    return this.http.post<IToken>(API_PATH + 'login/signin', user).pipe(
      tap((response: IToken) => {
        localStorage.setItem('acess_token', response.token);
        this._isLoggedIn.next(true);
      }),
    );
  }
}
