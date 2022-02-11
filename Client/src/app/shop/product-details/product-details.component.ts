import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  quantity = 1;
  product : IProduct ;
  paramsId  = 1;
  constructor(private shopService : ShopService , private activetadRoute : ActivatedRoute , private bcService : BreadcrumbService , private basketService : BasketService) {
    this.bcService.set('@productDetails', '')
   }

  ngOnInit(): void {
    this.paramsId = Number(this.activetadRoute.snapshot.paramMap.get("id") ) ;
    this.loadProduct();
  }

  loadProduct(){
    this.shopService.getProduct(this.paramsId).subscribe( product =>{
      this.product = product;
      this.bcService.set('@productDetails', product.name)
    } ,
    error =>{console.log(error)}) ;
  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product , this.quantity) ;
  }

  incrementQuantity(){
    this.quantity ++ ;
  }

  decrementQuantity(){
    if(this.quantity >1){
      this.quantity -- ;
    }

  }

}
