import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs/operators';
import { FormGroup, FormControl } from '@angular/forms';
import { retry } from 'rxjs/operators/retry';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { Validators } from '@angular/forms';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {

    private showError:boolean = false;

    private loginForm:FormGroup = new FormGroup({
        'username': new FormControl('', [Validators.required]),
        'password': new FormControl('', [Validators.required])
    })

    constructor(
        private authService: AuthService,
        private router: Router
    ) {}

    public onSubmit(): void {
        if (this.loginForm.invalid){
            this.showError = true;
            return;
        }
        this.authService.login(this.loginForm.value.username, this.loginForm.value.password)
            .pipe(
                tap(_ => this.router.navigate([''])),
                catchError(err => {
                    this.showError = true;
                    return ErrorObservable.create('invalid login');
                })
            )
            .subscribe();
    }
}