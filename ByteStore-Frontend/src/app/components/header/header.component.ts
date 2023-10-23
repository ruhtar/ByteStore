import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { TokenService } from 'src/app/services/token/token.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  constructor(
    public authService: AuthService,
    private tokenService: TokenService,
  ) {}
  logged!: boolean;
  username!: string;

  signOut() {
    localStorage.removeItem('acess_token');
    location.reload();
  }

  ngOnInit() {
    this.authService.isLoggedIn.subscribe((response) => {
      this.logged = response;
    });
    if (this.logged) {
      this.username = this.tokenService.getDecodedJwt().name;
    }
  }
}
