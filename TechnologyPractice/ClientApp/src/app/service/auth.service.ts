import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  uri = 'http://localhost:44369/api';
  token;
  public invalidLogin: boolean;

  constructor(private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) { }

  public loginAuth(email: string, password: string) {
    console.log(JSON.stringify({ username: email, password: password }));
    const credentials = JSON.stringify({ username: email, password: password });

    this.http.post('https://localhost:44369/api/authentication/login', credentials,
    {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe((resp: any) => { 
        console.log(resp.token);
        this.invalidLogin = false;
      localStorage.setItem('auth_token', resp.token);
      this.router.navigate(['/fetch-data']);
      }, err => {
        console.log(err);
        this.invalidLogin = true;
      });
  }

  public logout() {
    this.invalidLogin = false;
    localStorage.removeItem('auth_token');
  }

  public get logIn(): boolean {
    return (localStorage.getItem('auth_token') !== null);
  }

  public IsAuth(): boolean {
    const token = localStorage.getItem("auth_token");

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      console.log(this.jwtHelper.decodeToken(token));
      return true;
    }
    return false
  }
}
