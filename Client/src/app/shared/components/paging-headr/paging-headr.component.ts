import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-paging-headr',
  templateUrl: './paging-headr.component.html',
  styleUrls: ['./paging-headr.component.scss']
})
export class PagingHeadrComponent implements OnInit {
@Input() pageNumber : number ;
@Input() pageSize : number ;
@Input() totalCount : number ;

  constructor() { }

  ngOnInit(): void {
  }

}
