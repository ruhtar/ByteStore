import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserAggregate } from 'src/app/types/UserAggregate';
import { API_PATH } from 'src/environment/env';

@Injectable({
  providedIn: 'root',
})
export class SignupService {
  constructor(private http: HttpClient) {}

  signup(user: UserAggregate) {
    return this.http.post(API_PATH + 'login/signup', user);
  }
}
