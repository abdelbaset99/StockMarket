import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';
import { Observable } from 'rxjs';
import { UserSignIn } from './userSignIn';
import { UserSignUp } from './userSignUp';
import { JwtAuth } from './jwtAuth';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) {}

  private url = 'http://localhost:5089/api/User';

  public signIn(userSignIn: UserSignIn): Observable<JwtAuth> {
    const url = `${this.url}/signin`;
    return this.http.post<JwtAuth>(url, userSignIn);
  }

  public signUp(userSignUp: UserSignUp): Observable<JwtAuth> {
    const url = `${this.url}/signup`;
    return this.http.post<JwtAuth>(url, userSignUp);
  }
}
