import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.css'],
})
export class UserSettingsComponent {
  constructor(private router: Router) {}

  isRouterOutletEmpty(): boolean {
    return this.router.url === '/settings';
  }
}
