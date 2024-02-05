import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AutentificareService } from '../autentificare.service';
import { RouterOutlet } from '@angular/router';
import { LoginModel } from '../login.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterOutlet],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean = false;
  loggedIn: boolean = false;
  loggedOut: boolean = false;
  loginInfo: LoginModel = {userName: '', password: ''};
  mesajEroare: string = 'Formular invalid';

  form!: FormGroup;

  constructor(private autentificareService: AutentificareService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      userNameInput: new FormControl('', Validators.required),
      passwordInput: new FormControl('', Validators.required)
    });
  }

  onClickLogin(): void {
    if(this.form.valid) {
      this.loginInfo.userName = this.form.get('userNameInput')?.value;
      this.loginInfo.password = this.form.get('passwordInput')?.value;
      this.login();
      this.loggedIn = true;
      this.loggedOut = false;
    } else {
      this.invalidLogin = true;
    }
  }

  onClickLogout(): void {
    this.autentificareService.logout();
    this.loggedOut = true;
    this.loggedIn = false;
  }

  login(): void {
    this.autentificareService.login(this.loginInfo).subscribe(
      (response) => {
        console.log('Login successful');
        this.invalidLogin = false;
      },
      (error) => {
        console.error('Login error:', error);
        this.invalidLogin = true;
      }
    );
  }

  logout(): void {
    this.autentificareService.logout();
  }
}
