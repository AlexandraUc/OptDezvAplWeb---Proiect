import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { ArticolComponent } from './articol/articol.component';
import { ProfilComponent } from './profil/profil.component';
import { UtilizatorComponent } from './utilizator/utilizator.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ArticolComponent, ProfilComponent, UtilizatorComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Angular';

  titlu: string = 'Blog horror';
}
