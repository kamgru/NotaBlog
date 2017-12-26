import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { IToken } from '../models/IToken';

@Injectable()
export class AuthService {

    private authenticated:BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    private readonly accessTokenKey:string = "accessToken";
    private readonly refreshTokenKey:string = "refreshToken";
    private readonly expiresKey:string = "expires";

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
            return "Bearer " + token.accessToken;
        }
        return "";
    }

    public storeToken(token:IToken): void {
        localStorage.setItem(this.accessTokenKey, token.accessToken);
        localStorage.setItem(this.refreshTokenKey, token.refreshToken);
        localStorage.setItem(this.expiresKey, token.expires);
    }

    public invalidateToken(): void {
        localStorage.removeItem(this.accessTokenKey);
        localStorage.removeItem(this.refreshTokenKey);
        localStorage.removeItem(this.expiresKey);
        this.authenticated.next(false);
    }

    private getToken(): IToken | null {
        const accessToken = localStorage.getItem(this.accessTokenKey);
        const refreshToken = localStorage.getItem(this.refreshTokenKey);
        const expires = localStorage.getItem(this.expiresKey);

        if (accessToken && refreshToken && expires){
            return {
                accessToken: accessToken,
                refreshToken: refreshToken,
                expires: expires
            }
        }

        return null;
    }
}