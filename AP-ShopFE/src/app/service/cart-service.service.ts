import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { CartItem } from '../model/cart-item.model';
import { Product } from '../model/product.model';
import { AuthServiceService } from './auth-service.service';
import { OrderServiceService } from './order-service.service';
import { ProductServiceService } from './product-service.service';

@Injectable({
  providedIn: 'root'
})
export class CartServiceService {

  public cartItems = new BehaviorSubject<Product[]>([]);
  private userLoggedInId: number

  public selectedItemsId = new BehaviorSubject<number[]>([]);
  public selectedItems = new BehaviorSubject<any>([]);

  public buyNowItem = new BehaviorSubject<any>({});

  constructor(private http: HttpClient, private authService: AuthServiceService, private toastr: ToastrService, private productService: ProductServiceService, private orderService: OrderServiceService) { }

  getCartItems(userId: number) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', localStorage.getItem('token'))
    }
    return this.http.get<any>('https://localhost:7063/api/CartItem/' + userId, requestOptions).subscribe(result => {
      this.cartItems.next(result);
    });
  }

  logout() {
    this.cartItems.next([]);
  }

  addItemToCart(productId: number, quantity: number) {
    this.authService.userLoggedInId.subscribe(result => {
      this.userLoggedInId = result;
    })
    var item = new CartItem(this.userLoggedInId, productId, quantity);

    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
        .append('Authorization', localStorage.getItem('token'))
    }

    this.http.post('https://localhost:7063/api/CartItem', item, requestOptions).subscribe({
      next: () => {
        this.getCartItems(this.userLoggedInId);
        this.toastr.success("Item added to cart!");
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    });
  }

  buyNow(productId: number, quantity: number) {
    this.authService.userLoggedInId.subscribe(result => {
      this.userLoggedInId = result;
    })
    var item = new CartItem(this.userLoggedInId, productId, quantity)

    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
        .append('Authorization', localStorage.getItem('token'))
    }

    this.http.post('https://localhost:7063/api/CartItem/buyNow', item, requestOptions).subscribe({
      next: () => {
        this.toastr.success("You have successfully created order!");
        this.productService.getSearchProducts();
        this.orderService.getUserOrders(this.userLoggedInId);
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    });
  }

  removeItemFromCart(id: number) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', localStorage.getItem('token'))
    }
    this.http.delete('https://localhost:7063/api/CartItem/' + id, requestOptions).subscribe(result => {
      this.toastr.info("Item removed!");
      this.authService.userLoggedInId.subscribe(result => {
        this.userLoggedInId = result;
      })
      this.getCartItems(this.userLoggedInId);
    })
  }

  getSelectedItems(selectedItemsId: number[]) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
        .append('Authorization', localStorage.getItem('token'))
    }
    this.http.post<any>('https://localhost:7063/api/CartItem/selected', selectedItemsId, requestOptions).subscribe({
      next: (result) => {
        this.selectedItems.next(result);
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    })
  }

  buyItems() {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
        .append('Authorization', localStorage.getItem('token'))
    }
    this.http.post<any>('https://localhost:7063/api/CartItem/cashout', this.selectedItemsId.value, requestOptions).subscribe({
      next: (result) => {
        this.cartItems.next(result);
        this.toastr.success("Items successfully ordered!");
        this.productService.getSearchProducts();
        this.updateLoggedUserId();
        this.orderService.getUserOrders(this.userLoggedInId);
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    })
  }

  updateCartItemQuantity(cartItemId: number, quantity: number) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
    }
    this.http.put<any>('https://localhost:7063/api/CartItem/' + cartItemId + '?quantity=' + quantity, requestOptions).subscribe({
      next: (result) => {
        return result;
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    })
  }

  updateLoggedUserId() {
    this.authService.userLoggedInId.subscribe(result => {
      this.userLoggedInId = result;
    })
  }
}
