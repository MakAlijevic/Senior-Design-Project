import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Order } from '../model/order.modal';

@Injectable({
  providedIn: 'root'
})
export class OrderServiceService {

  userOrders = new BehaviorSubject<Order[]>([]);

  constructor(private http: HttpClient) { }

  getUserOrders(userId: number) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', localStorage.getItem('token'))
    }
    this.http.get<any>('https://localhost:7063/api/Order/user/' + userId, requestOptions).subscribe(result => {
      this.userOrders.next(result);
    })
  }


}
