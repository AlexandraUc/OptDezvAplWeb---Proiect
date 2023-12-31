import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { ArticolComponent } from './articol/articol.component';
import { ArticolService } from './articol.service';
import { ProfilComponent } from './profil/profil.component';
import { ProfilService } from './profil.service';
import { UtilizatorComponent } from './utilizator/utilizator.component';
import { UtilizatorService } from './utilizator.service';
import { LoginComponent } from './login/login.component';
import { AutentificareService } from './autentificare.service';

@NgModule({
  declarations: [AppComponent, ArticolComponent, ProfilComponent, UtilizatorComponent, LoginComponent],
  imports: [BrowserModule, HttpClientModule, ReactiveFormsModule, FormsModule],
  providers: [ArticolService, ProfilService, UtilizatorService, AutentificareService, JwtHelperService],
  bootstrap: [AppComponent],
})
export class AppModule {}