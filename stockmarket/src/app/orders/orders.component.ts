import { OrderService } from './../order.service';
import { Component } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Order } from '../order';


@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  title = 'orders';
  // orders: any[] = [];
  orders: Order[] = [];
  constructor(private http: HttpClient, private orderService: OrderService) {}

  ngOnInit(): void {
    this.getOrders();

  //   this.http.get<any[]>(`http://127.0.0.1:5089/api/Orders`).subscribe(
  //     orders => {
  //     this.orders = orders;
  //   }, err => {
  //     console.log(err);
  //   }
  // );
  }

  getOrders() {
    this.orderService.getOrders().subscribe(
      (orders) => {
        this.orders = orders;
      }
    );
  }
}
