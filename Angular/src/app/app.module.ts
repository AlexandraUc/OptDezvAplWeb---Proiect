import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ArticolComponent } from './articol/articol.component';
import { ArticolService } from './articol.service';
import { ProfilComponent } from './profil/profil.component';
import { ProfilService } from './profil.service';
import { UtilizatorComponent } from './utilizator/utilizator.component';
import { UtilizatorService } from './utilizator.service';

@NgModule({
  declarations: [AppComponent, ArticolComponent, ProfilComponent, UtilizatorComponent],
  imports: [BrowserModule, HttpClientModule, ReactiveFormsModule, FormsModule],
  providers: [ArticolService, ProfilService, UtilizatorService],
  bootstrap: [AppComponent],
})
export class AppModule {}