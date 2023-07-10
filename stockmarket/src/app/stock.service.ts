import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Stock } from './stock';
@Injectable({
  providedIn: 'root',
})
export class StockService {
  private url = 'http://localhost:5089/api/Stock';
  constructor(private http: HttpClient) {}

  getStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(this.url);
  }

  // getStock(id: number): Observable<Stock> {
  //   const url = `${this.url}/${id}`;
  //   return this.http.get<Stock>(url);
  // }
}
