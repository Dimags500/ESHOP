import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  /**
   * we check if we have basket id in local storege first , if do we get basket from basketService 
   */

 
  constructor(private basketService : BasketService ) {
      
  }
  ngOnInit(): void {
   const basketId = localStorage.getItem('basket_id');

   if(basketId ){
     this.basketService.getBasket(basketId).subscribe( () => {
       console.log('init basket');
       
     } , error => {
       console.log(error);
       
     });
   }
  }
}
