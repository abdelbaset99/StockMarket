import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Stock } from './stock';
import { Order } from './order';
import * as signalR from '@microsoft/signalr';
@Injectable({
  providedIn: 'root',
})
export class StockService {
  private stockUrl = 'http://localhost:5089/api/Stock';
  private orderUrl = 'http://localhost:5089/api/Order';
  constructor(private http: HttpClient, private hubConnection: signalR.HubConnection) {}

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.stockUrl, { headers: { Authorization: `Bearer ${localStorage.getItem('token')}` } });
  }
}
