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
  productId: string;
  productName: string;
  productSummary: string;
  userId: string;

  constructor() {
    this.id = "";
    this.action = "";
    this.productId = "";
    this.productName = "";
    this.productSummary = "";
    this.userId = "";
  }
}

export interface UserStore {
  email: string;
  role: string;
  isworker: boolean;
}
