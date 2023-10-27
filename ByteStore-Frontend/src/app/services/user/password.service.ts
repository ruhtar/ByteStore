import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_PATH } from 'src/environment/env';
import { TokenService } from '../token/token.service';

@Injectable({
  providedIn: 'root',
})
export class PasswordService {
  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) {}

  public changePassword(password: string, repassword: string) {
    const userId = this.tokenService.getDecodedJwt().nameid;
    const body = { Password: password, Repassword: repassword };
    return this.http.put(
      API_PATH + `user/change-password?userId=${userId}`,
      body,
      { observe: 'response', responseType: 'text' },
    );
  }

  public isPasswordValid(password: string): boolean {
    if (!/[A-Z]/.test(password)) {
      return false;
    }

    if (!/[^\w]/.test(password)) {
      return false;
    }

    if (!/\d/.test(password)) {
      return false;
    }

    if (password.length < 5) {
      return false;
    }

    return true;
  }
}
