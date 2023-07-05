import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';

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
  // stocks: Stock[] = [];
  constructor(private http: HttpClient) {}

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
  
  ngOnInit() {
    this.http.get<any[]>(`http://127.0.0.1:5000/`).subscribe(
      (stocks) => {
        // this.stocks = this.stockify(stocks);
        this.stocks = stocks;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  onsubmit(form: NgForm) {
    console.log(form.value);
    this.http.post<any[]>(`http://127.0.0.1:5000/`, form.value).subscribe(
      (response) => {
        console.log(response);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
