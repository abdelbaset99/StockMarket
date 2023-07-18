import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RouteGuard implements CanActivate {
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {

    const jwtHelper = new JwtHelperService();
    // Get the JWT token from the local storage
    const token = localStorage.getItem('jwt');

    // Check whether the token is expired and return
    // true or false
    if (token && !jwtHelper.isTokenExpired(token)) {
      console.log('token is valid');
      return true;
    }
    console.log('token is invalid');
    return false;
  }
  catch(error: any) {
    console.log(error);
    return false;
  }

}
