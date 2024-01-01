import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SchimbaParolaComponent } from './schimba-parola/schimba-parola.component';
import { AuthGuard } from './auth.guard';
import { UtilizatorComponent } from './utilizator/utilizator.component';
import { AdminAuthGuard } from './admin-auth.guard';

export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'schimba-parola', component: SchimbaParolaComponent, canActivate: [AuthGuard] },
    { path: 'utilizatori', component: UtilizatorComponent, canActivate: [AdminAuthGuard] }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}