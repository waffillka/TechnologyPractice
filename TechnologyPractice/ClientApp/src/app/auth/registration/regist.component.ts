import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';

import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-regist',
  templateUrl: './regist.component.html',
})

export class RegistComponent implements OnInit {


  constructor(private authService: AuthService) { }

  public registrAuth = (form: NgForm) => {
    const credentials = JSON.stringify(form.value);
    console.log(credentials);
    this.authService.registrationAuth(credentials);
  }

  ngOnInit() { }
}
