import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { IToken } from '../models/IToken';

@Injectable()
export class AuthService {

    private authenticated:BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    get isAuthenticated(): Observable<boolean> {

        let token = this.getToken();
        if (token != null) {
            let expires = new Date(token.expires);
            let now = new Date();

            this.authenticated.next(expires > now);
        }
        else {
            this.authenticated.next(false);
        }

        return this.authenticated.asObservable();
    }

    public getAuthorizationHeader(): string {
        const token = this.getToken();
        if (token){
            return "Bearer " + token.token;
        }
        return "";
    }

    public storeToken(token:IToken): void {
        localStorage.setItem('token', token.token);
        localStorage.setItem('expires', token.expires);
    }

    private getToken(): IToken | null {
        let token = localStorage.getItem('token');
        let expires = localStorage.getItem('expires');

        if (token != null && expires != null){
            return {
                token: token,
                expires: expires
            }
        }

        return null;
    }
}