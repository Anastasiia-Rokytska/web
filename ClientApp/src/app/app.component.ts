import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Role } from './common/models/authentication/role';
import { User } from './common/models/authentication/User';
import { AuthenticationService } from './core/components/authentication/authentication.service/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TSUKATTest';
  user: User;
  @Output() onRefreshed = new EventEmitter<any>();
  constructor(
    private authenticationService: AuthenticationService, 
    private route: Router) {
    this.authenticationService.user.subscribe(x => this.user = x);
    this.route.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
    });
  }

  isLoggedIn() {
    return this.user != null;
  }

  redirectTo(url) {
    window.location.href = url;
  }

  get isAdmin() {
    return this.user && this.user.roles.some((element) => element.name === Role.Administrator);
  }

  get isUser() {
    return this.user && this.user.roles.some((element) => element.name === Role.User);
  }

  hasRole(role: Role) {
    return this.user && this.user.roles.some((element) => element.name === role);
  }

  logout() {

    this.authenticationService.logout();
  }
}
