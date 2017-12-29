import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
    template: ''
})
export class LogoutComponent implements OnInit {

    constructor(private authService: AuthService, private router:Router){}

    public ngOnInit(): void{
        this.authService.logout();
        this.router.navigate(['login']);
    }
}