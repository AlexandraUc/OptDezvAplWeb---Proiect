import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Articol } from './articol/articol.model';
import { ArticolUtilizatorDto } from './articol/articol.model';

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
}