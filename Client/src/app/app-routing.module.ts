import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { ShopComponent } from './shop/shop.component';

const routes: Routes = [
  {path: '' , component: HomeComponent , data: {breadcrumb : 'home'} } ,
  {path : 'shop' , loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)
   , data: {breadcrumb : 'Shop'}   } ,
   {path : 'basket' , loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule)
   , data: {breadcrumb : 'basket'}   } ,
  {path : '**' , redirectTo : 'not-found' , pathMatch: 'full' } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
