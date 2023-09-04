import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  public getUserIdFromToken() {
    let jwt = localStorage.getItem('acess_token');

    if (jwt) {
      const decodedToken = this.decodeJwt(jwt);
      console.log('Informações do usuário do JWT:', decodedToken);
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
