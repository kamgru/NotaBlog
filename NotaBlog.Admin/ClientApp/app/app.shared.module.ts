import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar.component';
import { HomeComponent } from './shared/home.component';

@NgModule({
    declarations: [
        AppComponent, 
        NavbarComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent }
        ])
    ]
})
export class AppModuleShared {
}
