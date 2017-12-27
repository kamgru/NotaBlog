import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse, HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { catchError } from 'rxjs/operators/catchError';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { AuthService } from './auth.service';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router:Router, private authService: AuthService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
        
        const authRequest = request.clone({headers: request.headers.set('Authorization', this.authService.getAuthorizationHeader())})

        return next.handle(authRequest)
            .pipe(
                catchError(error => {
                    if (error instanceof HttpErrorResponse) {
                        if (error.status == 401){
                            this.authService.invalidateToken();
                            this.router.navigate(['login']);
                            return new EmptyObservable();
                        }
                    }
                    return error;
                })
            );
    }
    
}