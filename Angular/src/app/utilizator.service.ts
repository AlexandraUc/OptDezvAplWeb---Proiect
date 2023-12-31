import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Utilizator } from './utilizator/utilizator.model';
import { UtilizatorProfilDto } from './utilizator/utilizator.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UtilizatorService {
  private getUtilizatoriUrl = 'https://localhost:7295/api/Utilizatori';

  constructor(private http: HttpClient, private router: Router) {}

  getUtilizatori(): Observable<Utilizator[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);

    return this.http.get<Utilizator[]>(this.getUtilizatoriUrl, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          // Trebuie login
          this.router.navigate(['/login']);
        } else if (error.status === 403) {
          // E autentificat dar n are permisiunile necesare
          console.error('Forbidden access:', error);
        }
        return throwError(() => new Error('test'));
      })
    );
  }

  getUtilizatorId(id: string): Observable<Utilizator> {
    const getUtilizatorUrl = `${this.getUtilizatoriUrl}/${id}`;
    return this.http.get<Utilizator>(getUtilizatorUrl);
  }

  getUtilizatorProfil(userName: string): Observable<UtilizatorProfilDto> {
    const getUtilizatorProfilUrl = `${this.getUtilizatoriUrl}/cu_profil/${userName}`;
    return this.http.get<UtilizatorProfilDto>(getUtilizatorProfilUrl);
  }
}
