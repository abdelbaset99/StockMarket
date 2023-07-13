import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { OrdersComponent } from './orders/orders.component';
import { StocksComponent } from './stocks/stocks.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
// import { SocketsService } from './sockets.service';
// import { HubConnection } from '@microsoft/signalr/dist/esm/HubConnection';
import { HubConnection } from '@microsoft/signalr';
import { createHubConnection } from './hub-connection.factory';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent,
    OrdersComponent,
    StocksComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [{ provide: HubConnection, useFactory: createHubConnection }],
  bootstrap: [AppComponent]
})
export class AppModule { }
