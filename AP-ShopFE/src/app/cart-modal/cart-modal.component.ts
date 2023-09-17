import { Component, OnInit } from '@angular/core';
import { Product } from '../model/product.model';
import { CartServiceService } from '../service/cart-service.service';

@Component({
  selector: 'app-cart-modal',
  templateUrl: './cart-modal.component.html',
  styleUrls: ['./cart-modal.component.css']
})
export class CartModalComponent implements OnInit {

  cartItems: Product[];
  cartItemsCount: number;

  public selected = [];

  constructor(private cartService: CartServiceService) { }

  ngOnInit(): void {
    this.cartService.cartItems.subscribe(result => {
      this.cartItems = result;
      this.cartItemsCount = result.length;
    })
    this.cartService.selectedItemsId.subscribe(result => {
      this.selected = result;
    })
  }

  removeItemFromCart(id: number) {
    this.cartService.removeItemFromCart(id);
  }

  quantityChanged(event, cartItemId: number) {
    var inputValue = event.target.value;
    this.cartService.updateCartItemQuantity(cartItemId, inputValue);
  }

  setSelectedItem(item: string) {
    if (this.selected.includes(item)) {
      this.cartService.selectedItemsId.next(this.selected.filter(x => x !== item));
    }
    else {
      this.selected.push(item);
      this.cartService.selectedItemsId.next(this.selected);
    }
  }

  getSelectedItems() {
    this.cartService.getSelectedItems(this.selected);
  }

}
