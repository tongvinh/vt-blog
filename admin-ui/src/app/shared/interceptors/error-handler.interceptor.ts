import { HttpInterceptorFn, HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { AlertService } from '../services/alert.service';

@Injectable()
export class GlobalHttpInterceptorService implements HttpInterceptor {
  constructor(private alertService: AlertService) { }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(async ex => {
        console.log(ex);
        if (ex.status == 500) {
          this.alertService.showError('Hệ thống có lỗi xảy ra. Vui lòng liên hệ admin');
        }
        if (ex.status == 400) {
          const error = await (new Response(ex.error)).text();
          this.alertService.showError(error);
        }
        throw ex;
      })
    )
  }
};
