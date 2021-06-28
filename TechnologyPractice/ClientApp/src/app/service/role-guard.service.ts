import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
//import { User } from '../Models/User'

@Injectable()
export class RoleGuard implements CanActivate {
  role: string;
  private router: Router;
  constructor(role_: string, private path: string) {
    this.role = role_;
  }

  canActivate() {
    const token = localStorage.getItem("auth_user");
    const user : User = <User>JSON.parse(token);

    if (user == null) {
      return false;
    }
    else if (user.roles[0] != this.role) {
      console.log("role :" + this.role);
      return false;
    }
    

    this.router.navigate([this.path]);

    return true;
  }
}

export class User {
  id: string;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  phoneNumber: string
  roles: string[];
}
