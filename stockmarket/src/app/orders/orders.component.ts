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
  orders: Order[] = [];
  constructor(private http: HttpClient, private orderService: OrderService) {}

  ngOnInit(): void {
    this.getOrders();

  }

  getOrders() {
    this.orderService.getOrders().subscribe(
      (orders) => {
        this.orders = orders;
                console.log(this.orders);
      }
    );
  }
}
