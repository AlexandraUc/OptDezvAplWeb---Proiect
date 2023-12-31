import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterService } from '../register.service';
import { RouterOutlet } from '@angular/router';
import { RegisterModel } from './register.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterOutlet],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  invalidRegister: boolean = false;
  registerInfo: RegisterModel = {userName: '', email: '', password: '', confirmPassword: ''};
  rol: string = 'viewer';  // Default e viewer
  mesajEroare: string = 'Inregistrare esuata';

  form!: FormGroup;

  constructor(private registerService: RegisterService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      userNameInput: new FormControl('', Validators.required),
      emailInput: new FormControl ('', [Validators.required, Validators.email]),
      passwordInput: new FormControl('', Validators.required),
      confirmPasswordInput: new FormControl('', Validators.required)
    });
  }

  onClickRegister(): void {
    if(this.form.valid){
      this.registerInfo.userName = this.form.get('userNameInput')?.value;
      this.registerInfo.email = this.form.get('emailInput')?.value;
      this.registerInfo.password = this.form.get('passwordInput')?.value;
      this.registerInfo.confirmPassword = this.form.get('confirmPasswordInput')?.value;
      this.register();
    } else {
      this.invalidRegister = true;
    }
  }

  register(): void {
    this.registerService.register(this.registerInfo, this.rol).subscribe(
      (response) => {
        console.log('Register successful');
        this.invalidRegister = false;
        
        // Dupa inregistrare imediat logheaza utilizatorul
        this.loginAfterRegister();
      },
      (error) => {
        console.log('Register failed');
        this.invalidRegister = false;
      }
    );
  }

  loginAfterRegister(): void {
    this.registerService.loginRegister(this.registerInfo).subscribe(
      (response) => {
        console.log('Login successful after registration');
        this.invalidRegister = false;
      },
      (error) => {
        console.log('Login failed after registration');
        this.invalidRegister = false;
      }
    );
  }
}
