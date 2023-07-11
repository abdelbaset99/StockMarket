import { BuyRequest } from './../buy-request';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Stock } from '../stock';
import { Order } from '../order';
import { StockService } from '../stock.service';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css'],
})
export class StocksComponent {
  title = 'stocks';
  name: string = '';
  price: number = 0;
  quantity: number = 0;
  selectedStock: string = '';

  stocks: Stock[] = [];
  // private hubConnection!: signalR.HubConnection;
  constructor(private stockService: StockService) {}

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
    // this.hubConnection = new signalR.HubConnectionBuilder()
    //   .withUrl('http://localhost:5089/stockhub')
    //   .build();

    // this.hubConnection
    //   .start()
    //   .then(() => {
    //     console.log('Connection started');
    //     this.hubConnection.on('ReceiveStockPrices', (stocks) => {
    //       this.stocks = stocks;
    //       console.log(this.stocks);
    //     });
    //   })
    //   .catch((err) => console.log('Error while starting connection: ' + err));

    this.getStocks();

    // setInterval(() => {
    //   this.hubConnection.invoke('UpdateStockPrices');
    // }, 10000);
  }

  getStocks(): void {
    this.stockService.getStocks().subscribe((stocks) => (this.stocks = stocks));
  }


  onsubmit(form: NgForm) {
    console.log(form.value);

    const randomInt = Math.floor(Math.random() * 10000);

    const order: Order = {
      id: randomInt,
      stockID: this.getStockID(),
      buyerName: form.value.name,
      quantity: form.value.quantity,
      price: form.value.price,
    };
    // const request: BuyRequest = {
    //   StockName: this.selectedStock,
    //   Quantity: form.value.quantity,
    //   BuyerName: form.value.name,
    // };
    // this.stockService.buyStock(this.selectedStock, request).subscribe(
    //   (response) => {
    //     console.log(response);
    //   },
    //   (err) => {
    //     console.log(err);
    //   }
    // );
    this.stockService.buyStock(order).subscribe(
      (response) => {
        console.log(response);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
