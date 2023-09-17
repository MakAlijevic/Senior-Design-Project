import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditProductModalComponent } from './edit-product-modal/edit-product-modal.component';
import { HomepageComponent } from './homepage/homepage.component';
import { ProductListComponent } from './product-list/product-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/homepage', pathMatch: 'full' },
  {
    path: 'homepage', component: HomepageComponent
  },
  {
    path: 'products', component: ProductListComponent
  },
  {
    path: 'modify', component: EditProductModalComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
