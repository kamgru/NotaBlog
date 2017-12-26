import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs/Observable';
import { take, tap } from 'rxjs/operators';

@Injectable()
export class AuthGuard implements CanActivate {
    
    constructor(private authService: AuthService, private router:Router){}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.authService.isAuthenticated
            .pipe(
                take(1),
                tap(authenticated => authenticated 
                    ? () => {} 
                    : this.router.navigate(['login'])
                )
            );
    }
}