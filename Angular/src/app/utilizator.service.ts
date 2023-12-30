import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Utilizator } from './utilizator/utilizator.model';
import { UtilizatorProfilDto } from './utilizator/utilizator.model';
import { UtilizatorComponent } from './utilizator/utilizator.component';

@Injectable({
  providedIn: 'root'
})
export class UtilizatorService {
  private getUtilizatoriUrl = 'https://localhost:7295/api/Utilizatori';

  constructor(private http: HttpClient) {}

  getUtilizatori(): Observable<Utilizator[]> {
    return this.http.get<Utilizator[]>(this.getUtilizatoriUrl);
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
