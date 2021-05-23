import { Inject, Injectable, OnInit } from '@angular/core';
import { ProductItem, ProductHistory } from './product-model';
import { HttpClient } from "@angular/common/http";
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

/** product-service component*/
export class ProductService implements OnInit {

  public userRole: string;
  constructor(private authorizeService: AuthorizeService, private http: HttpClient, @Inject('BASE_URL') private _baseUrl: string) {
  }
  ngOnInit(): void {
    this.authorizeService.getUser().subscribe(role => {
      this.userRole = role.userRole;
    });
  }
  readonly _productUrl = "api/Products";
  readonly _productHistoryUrl = "api/ProductHistories";

  productFormData: ProductItem = new ProductItem();
  productList: ProductItem[];

  productHistoryFormData: ProductHistory = new ProductHistory();
  productHistoryList: ProductHistory[];

  postProduct() {
    return this.http.post(this._baseUrl + this._productUrl, this.productFormData);
  }
  putProduct() {
    return this.http.put(this._baseUrl + this._productUrl + "/" + this.productFormData.id, this.productFormData);
  }
  deleteProduct(id: number) {
    return this.http.delete(this._baseUrl + this._productUrl + "/" + this.productFormData.id);
  }
  refreshProductList() {
    this.http.get(this._baseUrl + this._productUrl)
      .toPromise()
      .then(res => this.productList = res as ProductItem[]);
  }

  refreshProductHistoryList() {
    var IsLimited = this.userRole === "Worker";
    this.http.get(this._baseUrl + this._productHistoryUrl + "?limit=" + IsLimited)
      .toPromise()
      .then(res => this.productHistoryList = res as ProductHistory[]);
  }
}
