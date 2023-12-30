import { Component, OnInit, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilService } from '../profil.service';
import { Profil } from './profil.model';
import { FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-profil',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profil.component.html',
  styleUrl: './profil.component.scss'
})
export class ProfilComponent implements OnInit, OnChanges, OnDestroy {
  profil!: Profil;
  profiluri: Profil[] = [];

  form!: FormGroup;

  constructor(private profilService: ProfilService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      idInput: new FormControl('')
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    
  }

  ngOnDestroy(): void {
    
  }

  onClickGetProfiluri(): void {
    this.getProfiluri();
  }

  onClickGetProfilId(): void {
    if(this.form){
      const id = this.form.get('idInput')?.value;
      if(id){
        this.getProfilId(id);
      }
    }
  }

  getProfiluri(): void {
    this.profilService.getProfiluri().subscribe((profiluri) => (this.profiluri = profiluri));
  }

  getProfilId(id: number): void {
    this.profilService.getProfilId(id).subscribe((profil) => (this.profil = profil));
  }
}
