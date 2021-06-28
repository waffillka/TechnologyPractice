import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
//import { User } from '../Models/User'

@Injectable()
export class RoleGuard implements CanActivate {
  private router: Router
  constructor(public path: string) {}

  canActivate() {
    const userJson = localStorage.getItem("auth_user");
    console.log(userJson);
    //const user: User = <User>JSON.parse(userJson);
    const roleAdmin = 'Administrator';
    const roleUser = 'User';

    /*if (user == null) {
      return false;
    }
    else if (user.roles[0] == roleAdmin) {
      console.log("role :" + roleAdmin);
      this.router.navigate([this.path]);
      return true;
    }
    else if (user.roles[0] == roleUser) {
      console.log("role :" + roleUser);
      this.router.navigate([this.path]);
      return true;
    }*/
    
    this.router.navigate(['']);
    return false;
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
