import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  public addJwtToHeaders(): HttpHeaders | undefined {
    const token = this.getDecodedJwt();
    if (token) {
      return new HttpHeaders({
        Authorization: `Bearer ${token}`,
      });
    }
    return undefined;
  }

  public getDecodedJwt() {
    let jwt = localStorage.getItem('acess_token');

    if (jwt) {
      const decodedToken = this.decodeJwt(jwt);
      return decodedToken;
    }
  }

  private decodeJwt(jwt: string): any {
    const tokenParts = jwt.split('.');
    if (tokenParts.length !== 3) {
      throw new Error('Invalid JWT');
    }

    const payload = atob(tokenParts[1]);
    return JSON.parse(payload);
  }
}
