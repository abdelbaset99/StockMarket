import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../user';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ElementRef, Renderer2 } from '@angular/core';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserService } from '../user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
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

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private userService: UserService,
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  ngOnInit(): void {}

  showNotification() {
    this.toastr.success(
      'User Registered Successfully',
      'Stock Market App',
      {
        timeOut: 3000,
        progressAnimation: 'increasing',
        positionClass: 'toast-top-right',
      }
    );
  }

  onSignUpSubmit(form: NgForm) {
    this.newUser.userName = this.newUserName;
    this.newUser.password = this.newPassword;
    this.newUser.email = this.newEmail;
    console.log(this.newUser);
    this.userService.signUp(this.newUser).subscribe(
      (res) => {
        console.log(res);
        this.showNotification();
      },
      (err) => {
        console.log(err);
      }
    );

  }

  onSignInSubmit(form: NgForm) {
    this.user.userName = this.userName;
    this.user.password = this.password;
    this.user.email = this.email;
    console.log(this.user);
    this.router.navigate(['/stocks']);
  }
}
