import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { UserLogin } from '../model/user.login.model';
import { UserRegister } from '../model/user.register.model';
import { OrderServiceService } from './order-service.service';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  public userLoggedIn = new BehaviorSubject<boolean>(false);

  public userRole = new BehaviorSubject<any>({});

  public userLoggedInId = new BehaviorSubject<any>({});

  constructor(private http: HttpClient, private toastr: ToastrService, private orderService: OrderServiceService) { }

  register(user: UserRegister) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
    }
    this.http.post('https://localhost:7063/api/Auth/Register', user).subscribe({
      next: () => {
        this.toastr.success("You have successfully registered!");
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    });
  }

  login(user: UserLogin) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json'),
      responseType: 'text'
    }

    this.http.post('https://localhost:7063/api/Auth/Login', user, requestOptions).subscribe({
      next: (token) => {
        localStorage.setItem("token", "bearer " + token);
        this.toastr.success("Successfully logged in");
        this.userLoggedIn.next(true);
        this.getLoggedUserId();
        this.getLoggedUserRole();
        this.orderService.getUserOrders(this.userLoggedInId.value);
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    });
  }

  logout() {
    localStorage.removeItem("token");
    this.userLoggedIn.next(false);
    this.userLoggedInId.next(0);
    this.userRole.next(1);
  }

  getTokenData() {
    var token = localStorage.getItem("token");
    return JSON.parse(atob(token.split('.')[1]));
  }

  getLoggedUserId() {
    var token = this.getTokenData();
    this.userLoggedInId.next(Object.values(token)[2]);
  }

  getLoggedUserRole() {
    var token = this.getTokenData();
    this.userRole.next(Object.values(token)[1]);
    console.log(this.userRole);
  }
}
