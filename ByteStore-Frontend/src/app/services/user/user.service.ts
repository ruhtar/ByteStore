import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Address } from 'src/app/types/Address';
import { API_PATH } from 'src/environment/env';
import { TokenService } from '../token/token.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) {}

  public getUserAddress() {
    const jwt = this.tokenService.getDecodedJwt();
    return this.http.get<Address>(API_PATH + `user/address/${jwt.nameid}`);
  }

  public editUserAddress(address: Address) {
    const jwt = this.tokenService.getDecodedJwt();
    return this.http.put(API_PATH + `user/address/${jwt.nameid}`, address);
  }
}
