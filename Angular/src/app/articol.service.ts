import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Articol } from './articol/articol.model';
import { ArticolUtilizatorDto } from './articol/articol.model';
import { ArticolFaraIdDto } from './articol/articol.model';

@Injectable({
  providedIn: 'root'
})

export class ArticolService {
  private getArticoleUrl = 'https://localhost:7295/api/Articole';

  // ArticolService are o proprietate http privata de tip HttpClient 
  // in care este injectat o instanta de HttpClient la creearea unui service
  constructor(private http: HttpClient) {}

  // Get generic
  getArticole(): Observable<Articol[]> {
    return this.http.get<Articol[]>(this.getArticoleUrl);
  }

  // Get grupate dupa autor ordonate dupa UtilizatorId si articolele ordonate dupa titlu
  getArticoleGrupate(): Observable<ArticolUtilizatorDto[]> {
    const getArticolGrupateUrl = 'https://localhost:7295/api/Articole/grupat_dupa';
    return this.http.get<any[]>(getArticolGrupateUrl);
  }

  // Get articole scrise de autor
  getArticoleAutor(autor: string): Observable<Articol[]> {
    const getArticoleAutorUrl = `${this.getArticoleUrl}/scris_de/${autor}`;
    return this.http.get<Articol[]>(getArticoleAutorUrl);
  }

  // Get dupa titlu
  getArticolTitlu(titlu: string): Observable<Articol> {
    const getArticolByTitluUrl = `${this.getArticoleUrl}/${titlu}`;
    return this.http.get<Articol>(getArticolByTitluUrl);
  }

  // Put articol dupa titlu
  putArticol(titlu: string, articol: ArticolFaraIdDto): Observable<ArticolFaraIdDto> {
    const putArticolUrl = `${this.getArticoleUrl}/${titlu}`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.put<ArticolFaraIdDto>(putArticolUrl, articol, { headers });
  }

  // Post articol
  postArticol(articol: ArticolFaraIdDto): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.post<any>(this.getArticoleUrl, articol, { headers });
  }

  // Delete articol de admin
  deleteArticol(id: number): Observable<any> {
    const deleteArticolUrl = `${this.getArticoleUrl}/${id}`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.delete<any>(deleteArticolUrl, { headers });
  }

  // Delete articol de catre autor
  deleteArticolAutor(titlu: string): Observable<any> {
    const deleteArticolAutorUrl = `${this.getArticoleUrl}/dupa/${titlu}`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.delete<any>(deleteArticolAutorUrl, { headers });
  }
}