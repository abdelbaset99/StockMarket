import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Stock } from './stock';
import { Order } from './order';
import { BuyRequest } from './buy-request';
@Injectable({
  providedIn: 'root',
})
export class StockService {
  private url = 'http://localhost:5089/api/Stock';
  constructor(private http: HttpClient) {}

  // buyStock(name: string, request: BuyRequest) {
  //   // console.log(name);

  //   const url = `${this.url}/${name}/buy`;
  //   return this.http.post(url, request);
  // }
  buyStock(order: Order): Observable<any> {
    const url = `${this.url}/${order.stockID}/buy`;
    return this.http.post(url, order);
  }

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.url);
  }
}
