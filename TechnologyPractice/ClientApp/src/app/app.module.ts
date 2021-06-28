import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient, HttpClientJsonpModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from "@auth0/angular-jwt";
import { CanActivate, Router } from '@angular/router';

import { AuthGuard } from './service/auth-guard.service';
import { RoleGuard } from './service/role-guard.service';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './auth/login.component'
import { RegistComponent } from './auth/registration/regist.component'
import { RegistAdminComponent } from './auth/registrationAdmin/registAdmin.component'

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegistComponent,
    RegistAdminComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent, canActivate: [AuthGuard, new RoleGuard('counter')]},
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard, new RoleGuard('fetch-data')] },
      { path: 'login', component: LoginComponent },
      { path: 'regist', component: RegistComponent },
      { path: 'registAdmin', component: RegistComponent },
    ]),
     JwtModule.forRoot({
       config: {
         tokenGetter: tokenGetter,
         whitelistedDomains: ["localhost:44369"],
         blacklistedRoutes: []
       }
     })
  ],
  providers: [
    AuthGuard,
    RoleGuard
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }

export function tokenGetter() {
  return localStorage.getItem("auth_token");
}

export function authGetter() {
  return localStorage.getItem('auth_bool') == "true";
}
