import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Stock } from '../stock';
import { Order } from '../order';
import { StockService } from '../stock.service';
import { OrderService } from '../order.service';
import { TranslateService } from '@ngx-translate/core';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css'],
})
export class StocksComponent {
  title = 'stocks';
  name: string = localStorage.getItem('userName') || '';
  stockPrice: number = 0;
  quantity: number = 1;
  selectedStock: string = '';

  stocks: Stock[] = [];

  @ViewChild('makeOrder') makeOrder : any;
  showModal(selectedStock: string, stockPrice: number) {
    this.selectedStock = selectedStock;
    this.stockPrice = stockPrice;
    this.makeOrder.nativeElement.showModal();
  }

  constructor(
    public translate: TranslateService, // inject TranslateService
    private stockService: StockService,
    private orderService: OrderService,
    private hubConnection: signalR.HubConnection
  ) {}

  getStockPrice() {
    const selectedStockObj = this.stocks.find(
      (stock) => stock.name === this.selectedStock
    );
    return selectedStockObj ? selectedStockObj.price : 0;
  }

  getStockID() {
    const selectedStockObj = this.stocks.find(
      (stock) => stock.name === this.selectedStock
    );
    return selectedStockObj ? selectedStockObj.id : 0;
  }

  ngOnInit(): void {
    this.startConnection();
    this.getStocks();

    setInterval(() => {
      if (this.hubConnection.state === signalR.HubConnectionState.Connected) {
        this.hubConnection
          .invoke('UpdateStockPrices')
          .then(() => {
            console.log('Stock prices updated successfully.');
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        console.warn('SignalR connection is not in the Connected state.');
      }
    }, 10000);
  }

  startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5089/stockhub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.hubConnection.on('ReceiveStockPrices', (stocks) => {
          this.stocks = stocks;
          console.log(this.stocks);
        });
      })
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  getStocks(): void {
    this.stockService.getStocks().subscribe((stocks) => (this.stocks = stocks));
  }

  onsubmit(form: NgForm) {
    console.log(form.value);

    const unixTimeInSeconds: number = Math.floor(Date.now() / 1000);

    const order: Order = {
      id: unixTimeInSeconds,
      stockID: this.getStockID(),
      buyerName: form.value.name,
      quantity: form.value.quantity,
      price: form.value.stockPrice,
    };
    this.orderService.makeOrder(order).subscribe(
      (response) => {
        console.log(response);
      },
      (err) => {
        console.log(err);
      }
    );

  }
}
