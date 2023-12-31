import { Component, OnInit, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UtilizatorService } from '../utilizator.service';
import { Utilizator } from './utilizator.model';
import { UtilizatorProfilDto } from './utilizator.model';
import { FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-utilizator',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './utilizator.component.html',
  styleUrl: './utilizator.component.scss'
})
export class UtilizatorComponent implements OnInit, OnChanges, OnDestroy {
  utilizator!: Utilizator;
  utilizatorProfil!: UtilizatorProfilDto;
  utilizatori: Utilizator[] = [];

  form!: FormGroup;
  form2!: FormGroup;
  formDelete!: FormGroup;

  constructor(private utilizatorService: UtilizatorService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      idInput: new FormControl('')
    });

    this.form2 = new FormGroup({
      userNameInput: new FormControl('')
    });

    this.formDelete = new FormGroup({
      userNameInput: new FormControl('')
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    
  }

  ngOnDestroy(): void {
    
  }

  onClickGetUtilizatori(): void {
    this.getUtilizatori();
  }

  onClickGetUtilizatorId(): void {
    if(this.form){
      const id = this.form.get('idInput')?.value;
      if(id)
        this.getUtilizatorId(id);
    }
  }

  onClickGetUtilizatorProfil(): void {
    if(this.form2){
      const userName = this.form2.get('userNameInput')?.value;
      if(userName)
        this.getUtilizatorProfil(userName);
    }
  }

  onClickDeleteUtilizator(): void {
    if(this.formDelete){
      const userName = this.formDelete.get('userNameInput')?.value;
      if(userName)
        this.deleteUtilizator(userName);
    }
  }

  getUtilizatori(): void {
    this.utilizatorService.getUtilizatori().subscribe((utilizatori) => (this.utilizatori = utilizatori));
  }

  getUtilizatorId(id: string): void {
    this.utilizatorService.getUtilizatorId(id).subscribe((utilizator) => (this.utilizator = utilizator));
  }

  getUtilizatorProfil(userName: string): void {
    this.utilizatorService.getUtilizatorProfil(userName).subscribe((utilizatorProfil) => (this.utilizatorProfil = utilizatorProfil));
  }

  deleteUtilizator(userName: string): void {
    this.utilizatorService.deleteUtilizator(userName).subscribe(
      () => {
        console.log('Utilizator sters');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }
}
