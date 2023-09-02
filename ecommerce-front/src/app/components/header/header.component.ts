import { Component } from '@angular/core';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  constructor(public loginService: LoginService) {}
  logged!: boolean;

  signOut() {
    localStorage.removeItem('acess_token');
    location.reload();
  }

  ngOnInit() {
    this.loginService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });
  }
}
