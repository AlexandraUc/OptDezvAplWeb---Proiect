import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, take, tap, throwError } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AutentificareService } from './autentificare.service';
import { RegisterModel } from './register/register.model';
import { RegisterResponse } from './register-response.model';
import { LoginModel } from './login.model';
import { LoginResponse } from './login-response.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private registerUrl = 'https://localhost:7295/api/Autentificare/register-';
  private loginUrl = 'https://localhost:7295/api/Autentificare/login';
  private jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private http: HttpClient, private autentificareService: AutentificareService) {}

  register(info: RegisterModel, rol: string): Observable<RegisterResponse> {
    const registerRolUrl = `${this.registerUrl}${rol}`;

    return this.http.post<RegisterResponse>(registerRolUrl, info).pipe(
      take(1),
      catchError((error: any) => {
        console.error('Registration failed:', error);
        return throwError(() => new Error('test'));
      })
    );
  }

  loginRegister(credentials: LoginModel): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.loginUrl, credentials).pipe(
      catchError((error: any) => {
        console.error('Login after registration failed:', error);
        return throwError(() => new Error('test'));
      }),
      tap((response: LoginResponse) => {
        localStorage.setItem('token', response.token);
      })
    );
  } 
}
