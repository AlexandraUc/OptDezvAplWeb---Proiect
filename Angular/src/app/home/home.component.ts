import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, Router } from '@angular/router';
import { ArticolComponent } from '../articol/articol.component';
import { ProfilComponent } from '../profil/profil.component';
import { UtilizatorComponent } from '../utilizator/utilizator.component';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'home-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ArticolComponent, ProfilComponent, UtilizatorComponent, LoginComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  constructor(private router: Router) {}

  onClickNavigateLogin(): void {
    this.router.navigate(['/login']);
  }

  onClickNavigateRegister(): void {
    this.router.navigate(['/register']);
  }

  onClickNavigateSchimbaParola(): void {
    this.router.navigate(['/schimba-parola']);
  }
}
