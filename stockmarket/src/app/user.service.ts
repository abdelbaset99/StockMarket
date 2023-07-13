import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) {}

  private url = 'http://localhost:5089/api/User';

  signIn(user: User): Observable<any> {
    const url = `${this.url}/signin`;
    return this.http.post(url, user);
  }

  signUp(user: User): Observable<any> {
    const url = `${this.url}/signup`;
    return this.http.post(url, user);
  }
}
