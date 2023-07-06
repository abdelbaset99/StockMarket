import { Component, OnInit } from '@angular/core';
import { SocketsService } from './sockets.service';
import { Observable, Subscription } from 'rxjs';
// import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'stocks';
  stocks: any[] = [];
  sub: Subscription = new Subscription();

  // constructor(private http: HttpClient) {}
  constructor(private socketService: SocketsService) {

  }

  ngOnInit() {
    this.getStocksData();
    // this.http.get<any[]>(`http://localhost:5000/`).subscribe(
    //   (stocks) => {
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
    });
  }

}
