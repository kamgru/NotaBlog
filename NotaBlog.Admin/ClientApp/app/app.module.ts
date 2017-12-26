import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { StoriesModule } from './stories/stories.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LogoutComponent } from './components/logout.component';
import { LoginComponent } from './components/login.component';
import { HomeComponent } from './components/home.component';
import { NavbarComponent } from './components/navbar.component';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './services/auth-interceptor.service';
import { LoginService } from './services/login.service';

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
        { provide: HTTP_INTERCEPTORS, multi: true, useClass: AuthInterceptor },
        AuthService, 
        LoginService,
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
