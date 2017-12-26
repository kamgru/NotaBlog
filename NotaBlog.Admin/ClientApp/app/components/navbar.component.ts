import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'navbar',
    templateUrl: './navbar.component.html',
})
export class NavbarComponent implements OnInit {

    private isAuthenticated:Observable<boolean>;

    constructor(private authService: AuthService) {}

    public ngOnInit(): void {
        this.isAuthenticated = this.authService.isAuthenticated
    }

}