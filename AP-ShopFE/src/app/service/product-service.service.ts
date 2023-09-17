import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { AddProduct } from '../model/add-product.modal';
import { Category } from '../model/category.model';
import { Product } from '../model/product.model';
import { CartServiceService } from './cart-service.service';

@Injectable({
  providedIn: 'root'
})
export class ProductServiceService {

  sortingType = 1;

  public isLoading = new BehaviorSubject<boolean>(false);

  public products = new BehaviorSubject<Product[]>([]);

  public editableProduct = new BehaviorSubject<Product>(null);

  public pageNumber = new BehaviorSubject<number>(1);

  public totalSearchedProductCount = new BehaviorSubject<number>(0);

  public searchCriteria = {
    categories: [],
    searchParam: "",
    minPrice: -1,
    maxPrice: -1,
    sortingType: 1,
    pageNumber: 0
  };

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  getProducts() {
    this.http.get<any>('https://localhost:7063/api/Product').subscribe(response => {
      this.products.next(response);
    });
  }

  setSearchParam(searchParam: string) {
    this.searchCriteria.searchParam = searchParam;
    this.getSearchProducts();
  }

  setSortingType(sortingType: number) {
    this.sortingType = sortingType;
    this.getSearchProducts();
  }

  getSearchProducts() {
    this.isLoading.next(true);
    setTimeout(() => {
      return this.http.get<any>('https://localhost:7063/api/Product/search', { params: this.searchCriteria }).subscribe(result => {
        this.products.next(result);
        this.getSearchedProductsCount();
        this.isLoading.next(false);
      })
    }, 1000)
    console.log(this.totalSearchedProductCount);

  }

  getSearchedProductsCount() {
    return this.http.get<any>('https://localhost:7063/api/Product/totalSearchedProducts', { params: this.searchCriteria }).subscribe(result => {
      this.totalSearchedProductCount.next(result);
    })
  }

  /* getSearchProducts() {
    this.isLoading.next(true);
    setTimeout(() => {
      if (this.searchParam === '' || this.searchParam == null) {
        return this.http.get<any>('https://localhost:7063/api/Product/sort/' + this.sortingType).subscribe(response => {
          this.products.next(response);
          this.isLoading.next(false);
        });
      }
      else {
        return this.http.get<any>('https://localhost:7063/api/Product/search/' + this.searchParam + "?sortingType=" + this.sortingType).subscribe(response => {
          this.products.next(response);
          this.isLoading.next(false);
        });
      }
    }, 1000)
  } */

  addProduct(product: AddProduct) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
        .append('Authorization', localStorage.getItem('token'))
    }
    this.http.post('https://localhost:7063/api/Product', product, requestOptions).subscribe({
      next: () => {
        this.toastr.success("Item successfully created!");
        this.getSearchProducts();
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    })
  }

  deleteProduct(productId: number) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Authorization', localStorage.getItem('token'))
    }
    this.http.delete('https://localhost:7063/api/Product/' + productId, requestOptions).subscribe({
      next: () => {
        this.toastr.error("Product Deleted!");
        this.getSearchProducts();
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    })
  }

  getEditableProduct(productId: number) {
    this.http.get<any>('https://localhost:7063/api/Product/' + productId).subscribe(result => {
      this.editableProduct.next(result);
    })
  }

  modifyProduct(productId: number, product: AddProduct) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
        .append('Authorization', localStorage.getItem('token'))
    }
    this.http.put('https://localhost:7063/api/Product/' + productId, product, requestOptions).subscribe({
      next: () => {
        this.toastr.warning("Product Updated!");
        this.getSearchProducts();
        this.editableProduct.next(null);
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    })
  }

  updateIsModified(productId: number) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append('Content-Type', 'application/json')
    }
    this.http.put('https://localhost:7063/api/Product/modify/' + productId, requestOptions).subscribe({
      next: () => {
      }
    })
  }

}
