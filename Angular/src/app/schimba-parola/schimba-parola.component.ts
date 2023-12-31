import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { SchimbaParola } from './schimba-parola.model';
import { SchimbaParolaService } from '../schimba-parola.service';

@Component({
  selector: 'app-schimba-parola',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterOutlet],
  templateUrl: './schimba-parola.component.html',
  styleUrl: './schimba-parola.component.scss'
})
export class SchimbaParolaComponent implements OnInit {
  schimbaParolaModel: SchimbaParola = {userName: '', currentPassword: '', newPassword: '', confirmPassword: ''};
  form!: FormGroup;

  constructor(private schimbaParolaService: SchimbaParolaService, private router: Router) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      userNameInput: new FormControl('', Validators.required),
      passwordInput: new FormControl('', Validators.required),
      newPasswordInput: new FormControl('', Validators.required),
      confirmPasswordInput: new FormControl('', Validators.required)
    });
  }

  onClickSchimbaParola(): void {
    if(this.form.valid){
      this.schimbaParolaModel.userName = this.form.get('userNameInput')?.value;
      this.schimbaParolaModel.currentPassword = this.form.get('passwordInput')?.value;
      this.schimbaParolaModel.newPassword = this.form.get('newPasswordInput')?.value;
      this.schimbaParolaModel.confirmPassword = this.form.get('confirmPasswordInput')?.value;

      this.schimbaParola();
    }
  }

  schimbaParola(): void {
    this.schimbaParolaService.schimbaParola(this.schimbaParolaModel).subscribe(
      () => {
        console.log('Parola schimbata');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }
}
