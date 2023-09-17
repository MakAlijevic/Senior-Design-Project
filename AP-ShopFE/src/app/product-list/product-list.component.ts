import { HttpClient } from '@angular/common/http';
import { Component, HostListener, OnInit } from '@angular/core';
import { Category } from '../model/category.model';
import { Product } from '../model/product.model';
import { AuthServiceService } from '../service/auth-service.service';
import { ProductServiceService } from '../service/product-service.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  private http;

  public categories: Category[];
  public products: Product[];

  public searchParam: string;

  isLoading: boolean;

  public selected = [];

  public userRole: number;

  public editableProductId: number;

  public pageNumber: number;

  public totalSearchedProductsCount: number;

  public minPrice = 1;
  public maxPrice = 1;

  constructor(http: HttpClient, private productService: ProductServiceService, private authService: AuthServiceService) {
    this.http = http;
  }

  ngOnInit(): void {
    this.loadCategories();
    this.productService.getSearchProducts();
    this.productService.products.subscribe(result => {
      this.products = result;
    })
    this.productService.isLoading.subscribe(result => {
      this.isLoading = result;
    })
    this.authService.userRole.subscribe(result => {
      this.userRole = result;
    })
    this.productService.editableProduct.subscribe(result => {
      this.editableProductId = result.id
    })
    this.productService.pageNumber.subscribe(result => {
      this.pageNumber = result;
    })
    this.productService.totalSearchedProductCount.subscribe(result => {
      this.totalSearchedProductsCount = result;
    })
    this.authService.getLoggedUserRole();
  }

  ngOnDestroy(): void {
    if (this.editableProductId != null) {
      this.editableProductId = null;
      this.productService.updateIsModified(this.editableProductId);
    }
  }

  @HostListener('window:beforeunload', ['$event'])
  beforeUnloadHandler($event) {
    if (this.editableProductId != null) {
      this.productService.updateIsModified(this.editableProductId);
      this.editableProductId = null;
      setTimeout(() => {

      }, 3000);
      /* $event.returnValue = true; */
    }
  }

  loadCategories() {
    this.http.get('https://localhost:7063/api/Category').subscribe(categories => {
      this.categories = categories;
    });
  }

  setSelected(id: string) {
    if (this.selected.includes(id)) {
      this.selected = this.selected.filter(x => x !== id);
      this.productService.searchCriteria.categories = this.selected;
      this.productService.getSearchProducts();
    }
    else {
      this.selected.push(id);
      this.productService.searchCriteria.categories = this.selected;
      this.productService.getSearchProducts();
    }
  }

  setSorting(sortingType: number) {
    this.productService.setSortingType(sortingType);
    this.productService.searchCriteria.sortingType = sortingType;
    console.log(this.productService.searchCriteria)
  }

  setPageNumber(pageNumber: number) {
    this.productService.pageNumber.next(pageNumber);
    this.productService.searchCriteria.pageNumber = this.pageNumber - 1;
    this.productService.getSearchProducts();
  }

  setMinPrice() {
    if (this.minPrice > 0) {
      this.productService.searchCriteria.minPrice = this.minPrice;
      this.productService.getSearchProducts();
    }
  }

  setMaxPrice() {
    if (this.maxPrice > 0) {
      this.productService.searchCriteria.maxPrice = this.maxPrice;
      this.productService.getSearchProducts();
    }
  }

  handlePriceFilter() {
    this.setMinPrice();
    this.setMaxPrice();
  }
}
