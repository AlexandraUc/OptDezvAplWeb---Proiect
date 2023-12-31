import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginModel } from './login.model';
import { LoginResponse } from './login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AutentificareService {
  private loginUrl = 'https://localhost:7295/api/Autentificare/login';
  private jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private http: HttpClient) {}

  login(credentials: LoginModel): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.loginUrl, credentials).pipe(
      catchError((error: any) => {
        console.error('Login failed:', error);
        return throwError(() => new Error());
      }),
      tap((response: LoginResponse) => {
        localStorage.setItem('token', response.token);
        this.getUserNameFromToken();
      })
    );
  }

  getUserNameFromToken(): string | null {
    const token = localStorage.getItem('token');

    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);

      if (decodedToken && decodedToken.hasOwnProperty('http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name')) {
        console.log(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
        return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      }
    }
    return null;
  }

  logout(): void {
    const userName = this.getUserNameFromToken();

    localStorage.removeItem('token');
    console.log('Logged out', userName);
  }

  verifAutentificare(): boolean {
    const token = localStorage.getItem('token');

    if(!token)
      return false;

    // Daca a expirat token ul il scoatem din local storage
    if(this.jwtHelper.isTokenExpired(token)){
      this.logout();
      return false;
    }

    return true;
  }
}
