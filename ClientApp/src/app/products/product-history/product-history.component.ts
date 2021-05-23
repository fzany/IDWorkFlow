import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../../shared/product-service';
import { ProductHistory } from '../../shared/product-model';

@Component({
    selector: 'app-product-history',
    templateUrl: './product-history.component.html',
    styleUrls: []
})
/** product-history component*/
export class ProductHistoryComponent implements OnInit {

  constructor(public service: ProductService) { }

  ngOnInit(): void {
    this.service.refreshProductHistoryList();
  }

  populateForm(selectedRecord: ProductHistory) {
    this.service.productHistoryFormData = Object.assign({}, selectedRecord);
  }

} 
