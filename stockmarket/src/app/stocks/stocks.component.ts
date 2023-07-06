import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { SocketsService } from '../sockets.service';



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
  price: number = 0;
  quantity: number = 0;
  // selectedStock: Stock = { stockID: 0, stockName: '', stockPrice: 0 };
  selectedStock: string = '';
  stocks: any[] = [];
  sub: Subscription = new Subscription();

  // stocks: Stock[] = [];
  constructor(private http: HttpClient, private socketService: SocketsService) {}

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

  ngOnInit() {
    this.getStocksData();
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

  getStocksData(): void {
    this.sub = this.socketService.getStocks().subscribe((stocks) => {
      this.stocks = stocks;
      // console.log("stock prices updated");
    });
  }
  
  onsubmit(form: NgForm) {
    const price = this.getStockPrice();
    const formData = { ...form.value, price }; // Include the price in the form data
    console.log(formData);
    // console.log(form.value);
    this.http.post<any[]>(`http://127.0.0.1:5000/`, formData).subscribe(
      (response) => {
        console.log(response);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
