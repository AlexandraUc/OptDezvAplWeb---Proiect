import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profil } from './profil/profil.model';
import { ProfilComponent } from './profil/profil.component';

@Injectable({
  providedIn: 'root'
})
export class ProfilService {
  private getProfiluriUrl = 'https://localhost:7295/api/Profiluri';

  constructor(private http: HttpClient) {}

  getProfiluri(): Observable<Profil[]> {
    return this.http.get<Profil[]>(this.getProfiluriUrl);
  }

  getProfilId(id: number): Observable<Profil> {
    const getProfilIdUrl = `${this.getProfiluriUrl}/${id}`;
    return this.http.get<Profil>(getProfilIdUrl);
  }
}
