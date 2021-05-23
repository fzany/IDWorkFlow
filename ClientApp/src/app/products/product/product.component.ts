import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../../shared/product-service';
import { ProductItem } from '../../shared/product-model';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: []
})
/** product component*/
export class ProductComponent implements OnInit {

  constructor(public service: ProductService) { }

  ngOnInit(): void {
    this.service.refreshProductList();
  }

  populateForm(selectedRecord: ProductItem) {
    this.service.productFormData = Object.assign({}, selectedRecord);
  }

  onEdit(item: ProductItem) {
   
  }

  onDelete(id: string) {
    console.log("deleting" + id);
    this.service.deleteProduct(id)
      .subscribe(
        res => {
          this.service.refreshProductList();
        },
        err => { console.log(err) }
      )
  }
} 
