import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
    template: ''
})
export class LogoutComponent implements OnInit {

    constructor(private authService: AuthService){}

    public ngOnInit(): void{
        this.authService.logout();
    }
}