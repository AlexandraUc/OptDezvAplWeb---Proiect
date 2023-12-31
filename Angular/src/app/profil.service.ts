import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profil } from './profil/profil.model';
import { PostProfilDto } from './profil/profil.model';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class ProfilService {
  private getProfiluriUrl = 'https://localhost:7295/api/Profiluri';

  constructor(private http: HttpClient) {}

  getProfiluri(): Observable<Profil[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get<Profil[]>(this.getProfiluriUrl, { headers });
  }

  getProfilId(id: number): Observable<Profil> {
    const getProfilIdUrl = `${this.getProfiluriUrl}/${id}`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get<Profil>(getProfilIdUrl, { headers });
  }

  putProfil(profil: PostProfilDto): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.put<any>(this.getProfiluriUrl, profil, { headers });
  }

  postProfil(profil: PostProfilDto): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.post<any>(this.getProfiluriUrl, profil, { headers });
  }

  deleteProfil(id: number): Observable<any> {
    const deleteProfilUrl = `${this.getProfiluriUrl}/${id}`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.delete<any>(deleteProfilUrl, { headers });
  }

  deleteProfilUtilizator(): Observable<any> {
    const deleteProfilUtilizatorUrl = `${this.getProfiluriUrl}/de-utilizator`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.delete<any>(deleteProfilUtilizatorUrl, { headers });
  }
}
