import { Component, Input, OnInit } from '@angular/core';
import { AddProduct } from '../model/add-product.modal';
import { Product } from '../model/product.model';
import { ProductServiceService } from '../service/product-service.service';

@Component({
  selector: 'app-add-product-modal',
  templateUrl: './add-product-modal.component.html',
  styleUrls: ['./add-product-modal.component.css']
})
export class AddProductModalComponent implements OnInit {

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
  }

  addProduct() {
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

    this.productService.addProduct(product);
  }

}
