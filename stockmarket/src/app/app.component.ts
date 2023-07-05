import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'stocks';
  stocks: any[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get<any[]>(`http://localhost:5000/`).subscribe(
      stocks => {
      this.stocks = stocks;
    }, err => {
      console.log(err);
    }
  );
}
}
