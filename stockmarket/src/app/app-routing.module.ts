import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './orders/orders.component';
import { AppComponent } from './app.component';
import { StocksComponent } from './stocks/stocks.component';

const routes: Routes = [
  // { path: '', component: AppComponent },
  { path: 'orders', component: OrdersComponent },
  // { path: 'stocks', component: StocksComponent },
  { path: '', component: StocksComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
