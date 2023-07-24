import { OrderService } from './../order.service';
import { Component } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Order } from '../order';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent {
  title = 'orders';
  orders: Order[] = [];
  constructor(private orderService: OrderService, public translate: TranslateService) {}

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
