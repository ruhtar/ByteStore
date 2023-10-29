import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Address } from 'src/app/types/Address';
import { API_PATH } from 'src/environment/env';
import { TokenService } from '../token/token.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  jwt!: any; //TODO: create type
  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
  ) {
    this.jwt = this.tokenService.getDecodedJwt();
  }

  public checkIfUserHasBoughtAProduct(productId: number) {
    return this.http.get(
      API_PATH +
        `user/purchase-history/check?userId=${this.jwt.nameid}&productId=${productId}`,
      {
        observe: 'response',
        responseType: 'text',
      },
    );
  }

  public getUserAddress() {
    return this.http.get<Address>(API_PATH + `user/address/${this.jwt.nameid}`);
  }

  public editUserAddress(address: Address) {
    return this.http.put(
      API_PATH + `user/address/${this.jwt.nameid}`,
      address,
      {
        observe: 'response',
        responseType: 'text',
      },
    );
  }

  public getUserPurchaseHistory() {
    return this.http.get<string>(
      API_PATH + `user/purchase-history?userId=${this.jwt.nameid}`,
    );
  }
}
