import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { IUser } from '../interfaces/IUser';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  baseUrl: string = "https://localhost:7062"
  constructor(private http: HttpClient) { }
  
  signIn(user: IUser){
    return this.http.post<string>(this.baseUrl + "/login/signin", user).subscribe();
  }
}
