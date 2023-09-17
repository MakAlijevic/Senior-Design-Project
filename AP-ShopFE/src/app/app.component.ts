import { Component, HostListener } from '@angular/core';
import { ProductServiceService } from './service/product-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AP-ShopFE';/* 
  editableProductId: number;

  constructor(private productService: ProductServiceService) { }

  ngOnInit(): void {
    this.productService.editableProduct.subscribe(result => {
      this.editableProductId = result.id;
    })
  }


  @HostListener('window:unload', ['$event'])
  unloadHandler(event) {
    if (this.editableProductId != null) {
      this.editableProductId = null;
      this.productService.updateIsModified(this.editableProductId);
    }
  } */
}
