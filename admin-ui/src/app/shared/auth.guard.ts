import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { TokenStorageService } from './services/token-storage.service';
import { UrlConstants } from './constants/url.constants';

@Injectable()
export class AuthGuard {
  constructor(private router: Router, private tokenService: TokenStorageService) { }

  canActivate(activateRoute: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): boolean {
    let requiredPolicy = activateRoute.data["requiredPolicy"] as string;
    var loggedInUser = this.tokenService.getUser();
    if (loggedInUser) {
      var listPermission = JSON.parse(loggedInUser.permissions);
      if (listPermission !== null && listPermission !== '' && listPermission.filter(x => x == requiredPolicy).length > 0) {
        return true;
      } else {
        this.router.navigate([UrlConstants.ACCESS_DENIED], {
          queryParams: {
            returnUrl: routerState.url
          }
        });
        return false;
      }
    } else {
      this.router.navigate([UrlConstants.LOGIN], {
        queryParams: {
          returnUrl: routerState.url
        }
      });
      return false;
    }
  }
}