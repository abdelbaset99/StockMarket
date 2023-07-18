import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { UserService } from '../user.service';
import { UserSignIn } from '../userSignIn';
import { UserSignUp } from '../userSignUp';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  signIn = new UserSignIn();
  signUp = new UserSignUp();

  newUserName: string = '';
  newPassword: string = '';
  newEmail: string = '';

  userName: string = '';
  password: string = '';

  constructor(private router: Router, private userService: UserService) {}

  ngOnInit(): void {}

  onSignUpSubmit(form: NgForm) {
    this.signUp.userName = this.newUserName;
    this.signUp.password = this.newPassword;
    this.signUp.email = this.newEmail;
    console.log(this.signUp);
    this.userService.signUp(this.signUp).subscribe(
      (res) => {
        if (res.success) {
          localStorage.setItem('jwt', res.token);
          this.router.navigate(['/stocks']);
        } else console.log(res);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  onSignInSubmit(form: NgForm) {
    this.signIn.userName = this.userName;
    this.signIn.password = this.password;
    console.log(this.signIn);
    this.userService.signIn(this.signIn).subscribe(
      (res) => {
        console.log(res.token);
        localStorage.setItem('jwt', res.token);
        localStorage.setItem('userName', this.userName);
        this.router.navigate(['/stocks']);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
