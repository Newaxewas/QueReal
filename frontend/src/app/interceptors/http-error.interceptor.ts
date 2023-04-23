import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(catchError((error: HttpErrorResponse)=>{
        switch(error.status){
          case 401: 
            this.router.navigateByUrl("/user/login");
            break;
          case 403: 
            this.router.navigateByUrl("/exception/no-access", {skipLocationChange: true});
            break;
          case 404: 
            this.router.navigateByUrl("/exception/not-found", {skipLocationChange: true});
            break;
        }

        return new Observable<HttpEvent<any>>();
      }));
  }
}
