import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  newUser: User = {} as User;
  newUserName: string = '';
  newPassword: string = '';
  newEmail: string = '';

  user: User = {} as User;
  userName: string = '';
  password: string = '';
  email: string = '';

  constructor() { }

  onSignUp

}
