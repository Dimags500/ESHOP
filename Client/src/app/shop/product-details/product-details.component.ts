import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product : IProduct ;
  paramsId  = 1;
  constructor(private shopService : ShopService , private activetadRoute : ActivatedRoute , private bcService : BreadcrumbService) {
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
    error =>{console.log(error);
    }
    ) ;
  }

}
