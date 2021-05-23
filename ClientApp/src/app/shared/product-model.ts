export class ProductItem {
  id: string;
  name: string;
  summary: string;
  constructor() {
    this.id = "";
    this.name = "";
    this.summary = "";
  }
}

export class ProductHistory {
  id: string;
  action: string;
  date: Date;
  productid: string;
  constructor() {
    this.id = "";
    this.action = "";
    this.productid = "";
  }
}

export interface UserStore {
  email: string;
  role: string;
  isworker: boolean;
}
