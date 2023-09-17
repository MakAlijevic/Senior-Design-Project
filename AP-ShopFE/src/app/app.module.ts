import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { RegisterModalComponent } from './register-modal/register-modal.component';
import { LoginModalComponent } from './login-modal/login-modal.component';
import { FormsModule } from '@angular/forms';
import { HomepageComponent } from './homepage/homepage.component';
import { ProductListComponent } from './product-list/product-list.component';
import { RouterModule } from '@angular/router';
import { ProductCardComponent } from './product-list/product-card/product-card.component';
import { CartModalComponent } from './cart-modal/cart-modal.component';
import { ProductDetailsModalComponent } from './product-list/product-details-modal/product-details-modal.component';
import { PurchaseHistoryModalComponent } from './purchase-history-modal/purchase-history-modal.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CashoutModalComponent } from './cashout-modal/cashout-modal.component';
import { AddProductModalComponent } from './add-product-modal/add-product-modal.component';
import { EditProductModalComponent } from './edit-product-modal/edit-product-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    RegisterModalComponent,
    LoginModalComponent,
    HomepageComponent,
    ProductListComponent,
    ProductCardComponent,
    CartModalComponent,
    ProductDetailsModalComponent,
    PurchaseHistoryModalComponent,
    CashoutModalComponent,
    AddProductModalComponent,
    EditProductModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
