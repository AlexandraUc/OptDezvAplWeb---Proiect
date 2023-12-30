import { Component, OnInit, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticolService } from '../articol.service';
import { Articol } from './articol.model';
import { ArticolUtilizatorDto } from './articol.model';
import { FormControl, FormGroup } from '@angular/forms';
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
  articole: Articol[] = [];
  articole2: Articol[] = [];
  articolUtilizatorDtoList: ArticolUtilizatorDto[] = [];

  public form!: FormGroup;
  public form2!: FormGroup;

  constructor(private articolService: ArticolService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      titluInput: new FormControl('')
    });

    this.form2 = new FormGroup({
      autorInput: new FormControl('')
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
}