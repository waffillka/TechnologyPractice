import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';

import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-regist',
  templateUrl: './regist.component.html',
})

export class RegistAdminComponent implements OnInit {


  constructor(private authService: AuthService) { }

  public registrAuthAdmin = (form: NgForm) => {
    const credentials = JSON.stringify(form.value);
    console.log(credentials);
    this.authService.registrationAuthAdmin(credentials);
  }

  ngOnInit() { }
}
