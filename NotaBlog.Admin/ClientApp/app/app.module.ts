import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoriesModule } from './stories/stories.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home.component';
import { NavbarComponent } from './components/navbar.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        StoriesModule,
        AppRoutingModule
    ],
    declarations: [
        NavbarComponent,
        HomeComponent,
        AppComponent
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
