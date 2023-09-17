import { Component, Input, NgModule, OnInit, ViewChild } from '@angular/core';
import { Product } from 'src/app/model/product.model';
import { AuthServiceService } from 'src/app/service/auth-service.service';
import { CartServiceService } from 'src/app/service/cart-service.service';


@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  public userLoggedIn: boolean;


  @Input() product: Product;

  constructor(private authService: AuthServiceService, private cartService: CartServiceService) { }

  ngOnInit(): void {
    this.authService.userLoggedIn.subscribe(result => {
      this.userLoggedIn = result;
    })
  }

  addToCart(productId: number, quantity: number) {
    if (!this.userLoggedIn) {
      $("#loginModal").modal('toggle');
    } else {
      this.cartService.addItemToCart(productId, quantity);
    }
  }

  buyNow(productId: number, quantity: number) {
    if (!this.userLoggedIn) {
      $("#loginModal").modal('toggle');
    } else {
      this.cartService.buyNow(productId, quantity)
    }
  }

}
