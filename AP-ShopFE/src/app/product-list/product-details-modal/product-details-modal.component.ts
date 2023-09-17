import { Component, HostListener, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/model/product.model';
import { AuthServiceService } from 'src/app/service/auth-service.service';
import { CartServiceService } from 'src/app/service/cart-service.service';
import { ProductServiceService } from 'src/app/service/product-service.service';

@Component({
  selector: 'app-product-details-modal',
  templateUrl: './product-details-modal.component.html',
  styleUrls: ['./product-details-modal.component.css']
})
export class ProductDetailsModalComponent implements OnInit {

  @Input() product: Product;

  @Input() userLoggedIn: boolean;

  userRole: number;

  constructor(private cartService: CartServiceService, private authService: AuthServiceService, private productService: ProductServiceService) { }

  ngOnInit(): void {
    this.authService.userRole.subscribe(result => {
      this.userRole = result;
    })
  }

  addToCart(productId: number) {
    if (!this.userLoggedIn) {
      $("#loginModal").modal('toggle');
    } else {
      var quantity = parseInt((document.getElementById("quantity" + productId) as HTMLInputElement)?.value);
      this.cartService.addItemToCart(productId, quantity);
    }
  }

  buyNow(productId: number) {
    if (!this.userLoggedIn) {
      $("#loginModal").modal('toggle');
    } else {
      var quantity = parseInt((document.getElementById("quantity" + productId) as HTMLInputElement)?.value);
      this.cartService.buyNow(productId, quantity);
      $("#Modal" + productId).modal('toggle');
    }
  }

  deleteProduct(productId: number) {
    this.productService.deleteProduct(productId);
  }

  modifyProduct(productId: number) {
    this.updateIsModified(productId);
    this.productService.getEditableProduct(productId);
  }

  updateIsModified(productId: number) {
    this.productService.updateIsModified(productId);
  }

  // @HostListener('window:beforeunload', ['$event'])
  // beforeUnloadHandler(event) {
  //   if (this.product.id != null) {
  //     this.product.id = null;
  //     this.productService.updateIsModified(this.product.id);
  //   }
  // }

}
