import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { AuthService } from './auth.service';
import { catchError } from 'rxjs/operators';
import { map, tap, concatMap } from 'rxjs/operators';
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private injector:Injector) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
        if (request.url == '/api/account/login' || request.url == '/api/account/renew-access'){
            return next.handle(request);
        }

        const authService:AuthService = this.injector.get(AuthService);
        const mapAuthRequest = map(authHeader => this.createAuthRequest(authHeader, request));
        const catchAuthError = catchError(error => this.handleAuthError(error, authService)); 
        const handleAuthRequest = concatMap((req:HttpRequest<any>) => next.handle(req).pipe(catchAuthError));

        return authService.getAuthorizationHeader()
            .pipe(
                mapAuthRequest,
                handleAuthRequest
            )
    }

    private createAuthRequest(header:any, request:HttpRequest<any>):HttpRequest<any>{
        return request.clone({
            headers: request.headers.set(header[0], header[1])
        });
    }

    private handleAuthError(error:any, authService:AuthService):Observable<any> {
        if (error instanceof HttpErrorResponse) {
            if (error.status == 401){
                authService.logout();
                return new EmptyObservable();
            }
        }
        return error;
    }
}