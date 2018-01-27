import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

@Component({
    selector: 'navbar',
    templateUrl: './navbar.component.html',
})
export class NavbarComponent implements OnInit {

    public hidden:boolean;

    constructor(private router:Router) {}

    public ngOnInit(): void {
        this.router.events.subscribe((event) => {
            if (event instanceof NavigationStart){
                this.hidden = event.url == '/login';
            }
        });
    }
}