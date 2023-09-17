import { Component, OnInit } from '@angular/core';
import { Order } from '../model/order.modal';
import { AuthServiceService } from '../service/auth-service.service';
import { OrderServiceService } from '../service/order-service.service';

@Component({
  selector: 'app-purchase-history-modal',
  templateUrl: './purchase-history-modal.component.html',
  styleUrls: ['./purchase-history-modal.component.css']
})
export class PurchaseHistoryModalComponent implements OnInit {

  userLoggedIn: number;
  userOrders: Order[];

  constructor(private orderService: OrderServiceService, private authService: AuthServiceService) { }

  ngOnInit(): void {
    this.authService.userLoggedInId.subscribe(result => {
      this.userLoggedIn = result;
    })
    this.getUserOrders();
    this.orderService.userOrders.subscribe(result => {
      console.log(result);
      this.userOrders = result
    })
  }

  getUserOrders() {
    this.orderService.getUserOrders(this.userLoggedIn);
    console.log()
  }

}
