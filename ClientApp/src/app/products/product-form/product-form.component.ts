import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ProductService } from '../../shared/product-service';
import { ProductItem } from '../../shared/product-model';

@Component({
    selector: 'app-product-form',
    templateUrl: './product-form.component.html',
    styleUrls: []
})
/** product-form component*/
export class ProductFormComponent  implements OnInit {
  constructor(public service: ProductService) {
  }
  ngOnInit(): void {
  }
  onSubmit(form: NgForm) {
    if (this.service.productFormData.id == "")
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }
  insertRecord(form: NgForm) {
    this.service.postProduct().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshProductList();
      },
      err => {
        console.log(err);
      }
    );
  }
  updateRecord(form: NgForm) {
    this.service.putProduct().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshProductList();
      },
      err => {
        console.log(err);
      }
    );
  }
  resetForm(form: NgForm) {
    form.form.reset();
    this.service.productFormData = new ProductItem();
  }
}
