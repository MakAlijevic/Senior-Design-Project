import { Component, OnInit } from '@angular/core';
import { CartServiceService } from '../service/cart-service.service';

@Component({
  selector: 'app-cashout-modal',
  templateUrl: './cashout-modal.component.html',
  styleUrls: ['./cashout-modal.component.css']
})
export class CashoutModalComponent implements OnInit {

  public selectedCartItems: any[]
  public totalPrice: number

  constructor(private cartService: CartServiceService) { }

  ngOnInit(): void {
    this.cartService.selectedItems.subscribe(result => {
      this.selectedCartItems = result;
      this.getTotalPrice();
    })
  }

  getTotalPrice() {
    this.totalPrice = 0;
    for (let i = 0; i < this.selectedCartItems.length; i++) {
      this.totalPrice += this.selectedCartItems[i].product.price * this.selectedCartItems[i].quantity
    }
  }

  buyItems() {
    this.cartService.buyItems();
  }

}
