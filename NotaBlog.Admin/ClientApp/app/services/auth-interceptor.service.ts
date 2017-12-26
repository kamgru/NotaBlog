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

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<any> {
        
        const authReq = req.clone({headers: req.headers.set('Authorization', this.authService.getAuthorizationHeader())})

        return next.handle(authReq)
            .pipe(
                catchError(x => {
                    if (x instanceof HttpErrorResponse) {
                        if (x.status == 401){
                            this.router.navigate(['login']);
                        }
                    }
                    return new EmptyObservable();
                })
            );
    }
    
}