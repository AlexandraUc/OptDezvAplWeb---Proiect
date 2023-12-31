import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { tap } from 'rxjs';
import { SchimbaParolaResponse } from './schimba-parola/schimba-parola.model';
import { SchimbaParola } from './schimba-parola/schimba-parola.model';

@Injectable({
  providedIn: 'root'
})
export class SchimbaParolaService {
  schimbaParolaUrl = 'https://localhost:7295/api/Autentificare/change-password';

  constructor(private http: HttpClient) {}

  schimbaParola(model: SchimbaParola): Observable<SchimbaParolaResponse> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.put<SchimbaParolaResponse>(this.schimbaParolaUrl, model, { headers });
  }
}
