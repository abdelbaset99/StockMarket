import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Stock } from './stock';
import { Order } from './order';
// import { BuyRequest } from './buy-request';
import * as signalR from '@microsoft/signalr';
@Injectable({
  providedIn: 'root',
})
export class StockService {
  private url = 'http://localhost:5089/api/Stock';
  constructor(private http: HttpClient, private hubConnection: signalR.HubConnection) {}

  // startConnection = () => {
  //   this.hubConnection = new signalR.HubConnectionBuilder()
  //     .withUrl('http://localhost:5089/stockhub', {
  //       skipNegotiation: true,
  //       transport: signalR.HttpTransportType.WebSockets,
  //     })
  //     .build();

  //   this.hubConnection
  //     .start()
  //     .then(() => {
  //       console.log('Connection started');
  //       this.hubConnection.on('ReceiveStockPrices', (stocks) => {
  //         this.stocks = stocks;
  //         console.log(this.stocks);
  //       });
  //     })
  //     .catch((err) => console.log('Error while starting connection: ' + err));

  //   // this.getStocks();

  //   setInterval(() => {
  //     this.hubConnection.invoke('UpdateStockPrices');
  //   }, 10000);
  // }
  buyStock(order: Order): Observable<any> {
    const url = `${this.url}/${order.stockID}/buy`;
    return this.http.post(url, order);
  }

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.url, { headers: { Authentication: `Bearer ${localStorage.getItem('token')}` } });
  }
}
