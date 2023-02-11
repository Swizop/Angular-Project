import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');
     if(token == null) {
       return next.handle(request);
     }
     var requestClone = request.clone({
       headers: request.headers.set('Authorization', `Bearer ${token}`)
     });
     requestClone = requestClone.clone({
      headers: requestClone.headers.set('Content-Type', 'application/json')
     });
     console.log(requestClone);

     return next.handle(requestClone);
  }
}
