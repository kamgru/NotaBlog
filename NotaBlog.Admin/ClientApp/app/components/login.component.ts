import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { catchError, tap } from 'rxjs/operators';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { LoginService } from '../services/login.service';

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
        private loginService: LoginService,
        private router: Router
    ) {}

    public onSubmit(): void {
        if (this.loginForm.invalid){
            this.showError = true;
            return;
        }
        this.loginService.login(this.loginForm.value.username, this.loginForm.value.password)
            .pipe(
                tap(_ => this.router.navigate([''])),
                catchError(err => {
                    this.showError = true;
                    return new EmptyObservable();
                })
            )
            .subscribe();
    }
}