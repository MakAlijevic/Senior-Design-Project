import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Product } from '../model/product.model';
import { AuthServiceService } from '../service/auth-service.service';
import { CartServiceService } from '../service/cart-service.service';
import { ProductServiceService } from '../service/product-service.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isHomepage = true;

  loggedUserId: string;
  userLoggedIn: boolean;

  cartItemsCount: number;

  searchParam: string;

  constructor(private router: Router, private productService: ProductServiceService, private authService: AuthServiceService, private cartService: CartServiceService) {
  }

  ngOnInit(): void {
    var token = localStorage.getItem("token");
    if (token) {
      this.authService.userLoggedIn.next(true);
      this.authService.getLoggedUserId();
    }
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isHomepage = event.url == '/homepage' || event.url == '/' ? true : false;
      }
    })
    this.authService.userLoggedIn.subscribe(result => {
      this.userLoggedIn = result;
    })
    this.authService.userLoggedInId.subscribe(result => {
      this.loggedUserId = result;
      this.cartService.getCartItems(parseInt(this.loggedUserId));
    });
    this.cartService.cartItems.subscribe(result => {
      this.cartItemsCount = result.length;
    })
  }

  setSearchParam() {
    this.productService.setSearchParam(this.searchParam);
    this.productService.searchCriteria.searchParam = this.searchParam;
    console.log(this.productService.searchCriteria)
  }

  logout() {
    this.authService.logout();
    this.cartService.logout();
  }

}
