import { Injectable } from '@angular/core';
import { io } from 'socket.io-client';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SocketsService {
  // private socket = io('http://localhost:5000');
  private socket: any;
  constructor() {
    this.socket = io('http://localhost:5000');
  }
  observer: any;
  getStocks(): Observable<any> {
    this.socket.on('stocks', (stocks: any[]) => {
      this.observer.next(stocks);
    });
    return this.getStocksObservable();
  }

  public getStocksObservable = () => {
    return new Observable((observer) => {
      this.observer = observer;
    });
  };
}
