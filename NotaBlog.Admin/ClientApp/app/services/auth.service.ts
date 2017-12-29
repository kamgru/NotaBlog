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

        return this.http.post<IToken>('/api/account/renew-access', this.token)
            .pipe(
                tap((token: IToken) => {
                    this.token = token;
                }),
                map((x:IToken) => true),
                catchError(x => {
                    return of(false);
                })
            )
    }

    public getAuthorizationHeader(): Observable<[string, string]> {
        if (this.token == null){
            const empty:[string, string] = ['', ''];
            return of(empty);
        }

        if (this.expired()) {
            return this.renewAccess()
                .pipe(
                    map(x => <[string, string]>['Authorization', 'Bearer ' + (this.token ? this.token.accessToken : '') ])
                )
        }
        else {
            const header:[string, string] = ['Authorization', 'Bearer ' + (this.token ? this.token.accessToken : '') ];
            return of(header);
        }
        
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