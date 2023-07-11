import { BuyRequest } from './../buy-request';
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { SocketsService } from '../sockets.service';
import { Stock } from '../stock';
import { StockService } from '../stock.service';



// interface Stock {
//   stockID: number;
//   stockName: string;
//   stockPrice: number;
// }



@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css'],
})
export class StocksComponent {
  title = 'stocks';
  name: string = '';
  // price: number = 0;
  quantity: number = 0;
  // stockID: number = 0;
  // selectedStock: Stock = { stockID: 0, stockName: '', stockPrice: 0 };
  selectedStock: string = '';
  // stocks: any[] = [];
  sub: Subscription = new Subscription();

  stocks: Stock[] = [];
  constructor(private http: HttpClient, private stockService: StockService) {}

  // stockify(starr: any[]) {
  //   let st: Stock[] = [];
  //   for (let i = 0; i < starr.length; i++) {
  //     console.log(starr[i]);
  //     let s = {
  //       stockID: starr[i]["id"],
  //       stockName: starr[i]["name"],
  //       stockPrice: starr[i]["price"]
  //     };
  //     st.push(s);
  //   }
  //   return st;
  // }

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
    this.getStocks();
    // console.log(this.stocks);
    // this.getStocksData();
    // this.http.get<any[]>(`http://127.0.0.1:5000/`).subscribe(
    //   (stocks) => {
    //     // this.stocks = this.stockify(stocks);
    //     this.stocks = stocks;
    //   },
    //   (err) => {
    //     console.log(err);
    //   }
    // );
  }

  getStocks(): void {
    this.stockService.getStocks().subscribe((stocks) => (this.stocks = stocks));
  }

  // getStocksData(): void {
  //   this.sub = this.socketService.getStocks().subscribe((stocks) => {
  //     this.stocks = stocks;
  //     // console.log("stock prices updated");
  //   });
  // }

  onsubmit(form: NgForm) {
    console.log(form.value);
    const request: BuyRequest = {
      StockName: this.selectedStock,
      Quantity: form.value.quantity,
      BuyerName: form.value.name,
    };
    this.stockService.buyStock(this.selectedStock, request).subscribe(
      (response) => {
        console.log(response);
      },
      (err) => {
        console.log(err);
      }
    );
    // const stockid = this.getStockID();
    // const price = this.getStockPrice();
    // // const stockName = this.selectedStock;
    // const formData = { ...form.value, stockid, price }; // Include the price in the form data
    // console.log(formData);
    // // console.log(form.value);
    // this.http.post<any[]>(`http://127.0.0.1:5089/api/Stock/${this.selectedStock}/buy`, formData).subscribe(
    //   (response) => {
    //     console.log(response);
    //   },
    //   (err) => {
    //     console.log(err);
    //   }
    // );
  }
}
