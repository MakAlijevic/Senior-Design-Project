import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ProductServiceService } from '../service/product-service.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  public searchParam: string;

  constructor(private productService: ProductServiceService) { }

  ngOnInit(): void {
  }

  setSearchParam() {
    this.productService.setSearchParam(this.searchParam);
    console.log(this.searchParam);
  }

}
