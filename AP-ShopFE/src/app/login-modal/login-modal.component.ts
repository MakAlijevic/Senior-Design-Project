import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserLogin } from '../model/user.login.model';
import { AuthServiceService } from '../service/auth-service.service';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.css']
})
export class LoginModalComponent implements OnInit {

  username: string;
  password: string;

  constructor(private authService: AuthServiceService) {
  }

  ngOnInit(): void {
  }

  login() {
    var user = new UserLogin();
    user.username = this.username;
    user.password = this.password;

    this.authService.login(user);
  }

}
