import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { tap } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { IToken } from '../models/IToken';

@Injectable()
export class LoginService {
    
    constructor(
        private http: HttpClient, 
        private authService: AuthService,
        private router:Router
    ) {}

    public login(username:string, password:string): Observable<Object> {
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/x-www-form-urlencoded');
        
        const params = new HttpParams()
            .set('Email', username)
            .set('Password', password);

        return this.http.post<IToken>('/api/account/login', params.toString(), {headers: headers})
            .pipe(
                tap((token: IToken) => this.authService.storeToken(token))
            );
    }

    public logout(): void {
        this.authService.invalidateToken();
        this.router.navigate(['login']);
    }
}