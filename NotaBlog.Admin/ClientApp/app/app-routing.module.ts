import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home.component';
import { AuthGuard } from './services/auth-guard.service';
import { LoginComponent } from './components/login.component';
import { LogoutComponent } from './components/logout.component';

const appRoutes:Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full', canActivate: [AuthGuard] },
    {path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
    {path: 'logout', component: LogoutComponent, canActivate: [AuthGuard]},
    {path: 'login', component: LoginComponent}
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ],
    providers: [AuthGuard]
})
export class AppRoutingModule {}