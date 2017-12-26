import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoriesModule } from './stories/stories.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home.component';
import { NavbarComponent } from './components/navbar.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthService } from './services/auth.service';
import { LoginComponent } from './components/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LogoutComponent } from './components/logout.component';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        StoriesModule,
        AppRoutingModule,
        ReactiveFormsModule
    ],
    declarations: [
        NavbarComponent,
        HomeComponent,
        LoginComponent,
        LogoutComponent,
        AppComponent
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl },
        AuthService
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
