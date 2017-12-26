import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of'
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { retry } from 'rxjs/operators/retry';

@Injectable()
export class AuthService {

    private authenticated:BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    get isAuthenticated(): Observable<boolean> {
        return this.authenticated.asObservable();
    }

    public login(username:string, password:string): Observable<boolean> {
        if (username == 'admin' && password == 'password'){
            this.authenticated.next(true);
            return this.authenticated.asObservable();
        }

        return ErrorObservable.create('invalid login attempt');
    }

    public logout(): void {
        this.authenticated.next(false);
    }
}