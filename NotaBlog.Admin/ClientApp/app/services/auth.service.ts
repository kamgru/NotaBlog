import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map, tap, catchError } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { IToken } from '../models/IToken';

@Injectable()
export class AuthService {

    private token:IToken | null;
    constructor(private http: HttpClient) {}

    public isAuthenticated():Observable<boolean> {
        if (this.expired()){
            return this.renewAccess();
        }

        return of(true);
    }

    private renewAccess(): Observable<boolean>{
        if (this.token == null){
            return of(false);
        }

        return this.http.post<IToken>('/api/account/renew-access', {})
            .pipe(
                tap((token: IToken) => {
                    this.token = token;
                }),
                map((x:IToken) => true),
                catchError(_ => of(false))
            )
    }

    public getAuthorizationHeader(): [string, string] {
        return ['Authorization', 'Bearer ' + (this.token ? this.token.accessToken : '') ];
    }

    public login(username:string, password:string): Observable<Object> {
        const headers = new HttpHeaders()
            .set('Content-Type', 'application/x-www-form-urlencoded');
        
        const params = new HttpParams()
            .set('Email', username)
            .set('Password', password);

        return this.http.post<IToken>('/api/account/login', params.toString(), {headers: headers})
            .pipe(
                tap((token: IToken) => {
                    this.token = token;
                })
            );
    }

    public logout(): void {
        this.token = null;
    }

    private expired(): boolean {   
        return this.token 
            ? new Date(this.token.expires) < new Date() 
            : true;
    }
}