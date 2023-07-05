import { Component } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  title = 'orders';
  orders: any[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<any[]>(`http://127.0.0.1:5000/orders`).subscribe(
      orders => {
      this.orders = orders;
    }, err => {
      console.log(err);
    }
  );
}
}
