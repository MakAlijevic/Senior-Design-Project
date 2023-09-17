import { Component, HostListener, Input, OnInit } from '@angular/core';
import { AddProduct } from '../model/add-product.modal';
import { Product } from '../model/product.model';
import { ProductServiceService } from '../service/product-service.service';

@Component({
  selector: 'app-edit-product-modal',
  templateUrl: './edit-product-modal.component.html',
  styleUrls: ['./edit-product-modal.component.css']
})
export class EditProductModalComponent implements OnInit {

  product: Product;

  productName: string;
  imagePath: string;
  price: number;
  description: string;
  quantity: number;
  categoryId: number;
  conditionId: number;
  shippingId: number;

  constructor(private productService: ProductServiceService) { }

  ngOnInit(): void {
    this.productService.editableProduct.subscribe(result => {
      this.product = result;
      this.productName = result.productName;
      this.imagePath = result.imagePath;
      this.price = result.price;
      this.description = result.description;
      this.quantity = result.quantity;
      this.categoryId = result.category.id;
      this.conditionId = result.condition.id;
      this.shippingId = result.shipping.id;
    })
  }

  updateProduct(productId: number) {

    var product = new AddProduct(
      this.productName,
      this.imagePath,
      this.price,
      this.description,
      this.quantity,
      this.categoryId,
      this.conditionId,
      this.shippingId
    );

    this.productService.modifyProduct(productId, product);
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
