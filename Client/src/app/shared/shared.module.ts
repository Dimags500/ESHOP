import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import { PagingHeadrComponent } from './components/paging-headr/paging-headr.component';
import { PagerComponent } from './components/pager/pager.component'


@NgModule({
  declarations: [
    PagingHeadrComponent,
    PagerComponent
  ],
  imports: [
    CommonModule ,
    PaginationModule.forRoot()
  ] ,
  exports : [
    PaginationModule , 
    PagingHeadrComponent ,
    PagerComponent
  
    ]

})
export class SharedModule { }
