import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserAggregate } from 'src/app/interfaces/IUserAggregate';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class SignupService {
  constructor(private http: HttpClient) {}

  signup(user: IUserAggregate) {
    return this.http.post(API_PATH + 'login/signup', user);
  }
}
