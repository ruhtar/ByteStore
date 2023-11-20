import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { IToken } from 'src/app/types/IToken';
import { User } from 'src/app/types/User';
import { UserAggregate } from 'src/app/types/UserAggregate';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _isLoggedIn = new BehaviorSubject<boolean>(false);
  isLoggedIn = this._isLoggedIn.asObservable();

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('access_token');
    //without this, if we reload the page, it counts as if we are not logged in
    this._isLoggedIn.next(!!token); //transforms the token value in a boolean
  }

  signup(user: UserAggregate) {
    return this.http.post(API_PATH + 'users/signup', user, {
      observe: 'response',
      responseType: 'text',
    });
  }

  signIn(user: User) {
    return this.http.post<IToken>(API_PATH + 'users/signin', user).pipe(
      tap((response: IToken) => {
        localStorage.setItem('access_token', response.token);
        this._isLoggedIn.next(true);
      }),
    );
  }
}
