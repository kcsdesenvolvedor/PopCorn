import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpResponseBase
} from '@angular/common/http';

import { Observable, tap } from 'rxjs';
import { Global } from 'src/shared/Global';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  intercept(request: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    const baseUrl = request.url.split('/');
    const apiUrl = Global.BASE_URL_API.split('/');
    
    if(localStorage.getItem('user')){
        const token = JSON.parse(localStorage.getItem('user')).token;
        if(token && (baseUrl[2] === apiUrl[2])){  // (baseUrl[2] === apiUrl[2]) -> verifica se a requisição está sendo para a url da nossa api, caso haja requisições para outras api´s externas.
            const newRequest = request.clone({
                setHeaders: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return next.handle(newRequest);
        }
        return next.handle(request);
    }else{
        return next.handle(request);
    }

    
  }
}