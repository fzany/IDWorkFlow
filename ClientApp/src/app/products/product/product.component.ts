import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../../shared/product-service';
import { ProductItem } from '../../shared/product-model';
import { AuthorizeService } from '../../../api-authorization/authorize.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: []
})
/** product component*/
export class ProductComponent implements OnInit {

  public userRole: string;
  constructor(public service: ProductService, private authorizeService: AuthorizeService) { }

  ngOnInit(): void {
    this.service.refreshProductList();
    this.authorizeService.getUser().subscribe(role => {
      this.userRole = role.userRole;
    });
  }

  populateForm(selectedRecord: ProductItem) {
    this.service.productFormData = Object.assign({}, selectedRecord);
  }

  onEdit(item: ProductItem) {
    console.log(item);
  }

  onDelete(id: string) {
    this.service.deleteProduct(id)
      .subscribe(
        res => {
          this.service.refreshProductList();
        },
        err => { console.log(err) }
      )
  }
} 
