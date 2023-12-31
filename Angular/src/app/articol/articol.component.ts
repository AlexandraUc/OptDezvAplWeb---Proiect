import { Component, OnInit, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticolService } from '../articol.service';
import { Articol } from './articol.model';
import { ArticolUtilizatorDto } from './articol.model';
import { ArticolFaraIdDto } from './articol.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-articol',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './articol.component.html',
  styleUrl: './articol.component.scss',
})
export class ArticolComponent implements OnInit, OnChanges, OnDestroy {
  articol!: Articol;
  articolPut: ArticolFaraIdDto = {titlu: '', continut: ''};
  articolPost: ArticolFaraIdDto = {titlu: '', continut: ''};
  articole: Articol[] = [];
  articole2: Articol[] = [];
  articolUtilizatorDtoList: ArticolUtilizatorDto[] = [];

  formInvalid: boolean = false;
  mesajEroare: string = 'Formular invalid';

  form!: FormGroup;
  form2!: FormGroup;
  formPut!: FormGroup;
  formPost!: FormGroup;
  formDelete!: FormGroup;
  formDeleteAutor!: FormGroup;

  constructor(private articolService: ArticolService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      titluInput: new FormControl('')
    });

    this.form2 = new FormGroup({
      autorInput: new FormControl('')
    });

    this.formPut = new FormGroup({
      titluInput: new FormControl('', Validators.required),
      titluModificatInput: new FormControl('', Validators.required),
      continutModificatInput: new FormControl('', Validators.required)
    });

    this.formPost = new FormGroup({
      titluInput: new FormControl('', Validators.required),
      continutInput: new FormControl('', Validators.required)
    });

    this.formDelete = new FormGroup({
      idInput: new FormControl('', Validators.required)
    });

    this.formDeleteAutor = new FormGroup({
      titluInput: new FormControl('', Validators.required)
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    
  }

  ngOnDestroy(): void {
    
  }

  onClickGetArticole(): void {
    this.getArticole();
  }

  onClickGetArticoleGrupate(): void {
    this.getArticoleGrupate();
  }

  onClickGetArticolTitlu(): void {
    if (this.form){
      const titlu = this.form.get('titluInput')?.value;
      if (titlu){
        this.getArticolTitlu(titlu);
      }
    } else {
      this.formInvalid = true;
    }
  }

  onClickGetArticoleAutor(): void {
    if(this.form2){
      const autor = this.form2.get('autorInput')?.value;
      if(autor){
        this.getArticoleAutor(autor);
      }
    }
  }

  onClickPutArticol(): void {
    if(this.formPut.valid){
      const titlu = this.formPut.get('titluInput')?.value;
      this.articolPut.titlu = this.formPut.get('titluModificatInput')?.value;
      this.articolPut.continut = this.formPut.get('continutModificatInput')?.value;

      this.putArticol(titlu);
    }
  }

  onClickPostArticol(): void {
    if(this.formPost.valid){
      this.articolPost.titlu = this.formPost.get('titluInput')?.value;
      this.articolPost.continut = this.formPost.get('continutInput')?.value;
      
      this.postArticol();
    }
  }

  onClickDeleteArticol(): void {
    if(this.formDelete.valid){
      const id = this.formDelete.get('idInput')?.value;

      this.deleteArticol(id);
    }
  }
  
  onClickDeleteArticolAutor(): void {
    if(this.formDeleteAutor.valid){
      const titlu = this.formDeleteAutor.get('titluInput')?.value;

      this.deleteArticolAutor(titlu);
    }
  }

  getArticole(): void {
    this.articolService.getArticole().subscribe((articole) => (this.articole = articole));
  }

  getArticoleGrupate(): void {
    this.articolService.getArticoleGrupate().subscribe(
      (response) => {
        this.articolUtilizatorDtoList = response;
      },
      (error) => {
        console.error('API Error:', error);
      }
    );
  }

  getArticoleAutor(autor: string): void {
    this.articolService.getArticoleAutor(autor).subscribe((articole2) => (this.articole2 = articole2));
  }

  getArticolTitlu(titlu: string): void {
    this.articolService.getArticolTitlu(titlu).subscribe((articol) => (this.articol = articol));
  }

  putArticol(titlu: string): void {
    this.articolService.putArticol(titlu, this.articolPut).subscribe(
      response => {
        console.log('Articol modificat:', response);
      },
      error => {
        console.error('Eroare: ', error);
      }
    );
  }

  postArticol(): void {
    this.articolService.postArticol(this.articolPost).subscribe(
      () => {
        console.log('Articol adaugat');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }

  deleteArticol(id: number): void {
    this.articolService.deleteArticol(id).subscribe(
      () => {
        console.log('Articol sters');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }

  deleteArticolAutor(titlu: string): void {
    this.articolService.deleteArticolAutor(titlu).subscribe(
      () => {
        console.log('Articol sters');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }
}