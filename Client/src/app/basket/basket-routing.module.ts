import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket.component';

const rouths : Routes = [
  {path: '' , component : BasketComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule , 
    RouterModule.forChild(rouths)
  ] ,
  exports : [RouterModule]
})
export class BasketRoutingModule { }
