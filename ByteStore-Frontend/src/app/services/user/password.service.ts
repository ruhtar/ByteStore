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
    const userId = this.tokenService.getJwtFromLocalStorage().nameid;
    const body = { Password: password, Repassword: repassword };
    console.log(password);
    console.log(repassword);
    console.log(body);
    return this.http.put(
      API_PATH + `user/change-password?userId=${userId}`,
      body,
    );
  }
}
