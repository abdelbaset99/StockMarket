import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from './order';
// import { BuyRequest } from './buy-request';
@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private url = 'http://localhost:5089/api/Order';
  constructor(private http: HttpClient) {}


  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.url);
  }

}
