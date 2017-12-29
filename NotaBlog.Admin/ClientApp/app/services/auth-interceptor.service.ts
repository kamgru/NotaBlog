import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { AuthService } from './auth.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private injector:Injector) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
        if (request.url == '/api/account/login' || request.url == '/api/account/renew-access'){
            return next.handle(request);
        }

        const authService = this.injector.get(AuthService);
        const header = authService.getAuthorizationHeader();
        const authRequest = request.clone({
            headers: request.headers.set(header[0], header[1])
        });

        return next.handle(authRequest)
            .pipe(
                catchError(error => {
                    if (error instanceof HttpErrorResponse) {
                        if (error.status == 401){
                            authService.logout();
                            return new EmptyObservable();
                        }
                    }
                    return error;
                })
            );
    }
}