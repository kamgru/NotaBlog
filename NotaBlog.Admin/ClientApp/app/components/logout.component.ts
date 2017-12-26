import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';

@Component({
    template: ''
})
export class LogoutComponent implements OnInit {

    constructor(private loginService: LoginService){}

    public ngOnInit(): void{
        this.loginService.logout();
    }
}