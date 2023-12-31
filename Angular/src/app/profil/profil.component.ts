import { Component, OnInit, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilService } from '../profil.service';
import { Profil } from './profil.model';
import { PostProfilDto } from './profil.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  profilPut: PostProfilDto = {nume: '', prenume: '', bio: ''};
  profilPost: PostProfilDto = {nume: '', prenume: '', bio: ''};

  form!: FormGroup;
  formPut!: FormGroup;
  formPost!: FormGroup;
  formDelete!: FormGroup

  constructor(private profilService: ProfilService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      idInput: new FormControl('')
    });

    this.formPut = new FormGroup({
      numeInput: new FormControl('', Validators.required),
      prenumeInput: new FormControl('', Validators.required),
      bioInput: new FormControl('', Validators.required)
    });

    this.formPost = new FormGroup({
      numeInput: new FormControl('', Validators.required),
      prenumeInput: new FormControl('', Validators.required),
      bioInput: new FormControl('', Validators.required)
    });

    this.formDelete = new FormGroup({
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

  onClickPutProfil(): void {
    if(this.formPut){
      this.profilPut.nume = this.formPut.get('numeInput')?.value;
      this.profilPut.prenume = this.formPut.get('prenumeInput')?.value;
      this.profilPut.bio = this.formPut.get('bioInput')?.value;

      this.putProfil();
    }
  }

  onClickPostProfil(): void {
    if(this.formPost){
      this.profilPost.nume = this.formPost.get('numeInput')?.value;
      this.profilPost.prenume = this.formPost.get('prenumeInput')?.value;
      this.profilPost.bio = this.formPost.get('bioInput')?.value;

      this.postProfil();
    }
  }

  onClickDeleteProfil(): void {
    if(this.formDelete){
      const id = this.formDelete.get('idInput')?.value;
      if(id){
        this.deleteProfil(id);
      }
    }
  }

  onClickDeleteProfilUtilizator(): void {
    this.deleteProfilUtilizator();
  }

  getProfiluri(): void {
    this.profilService.getProfiluri().subscribe((profiluri) => (this.profiluri = profiluri));
  }

  getProfilId(id: number): void {
    this.profilService.getProfilId(id).subscribe((profil) => (this.profil = profil));
  }

  putProfil(): void {
    this.profilService.putProfil(this.profilPut).subscribe(
      () => {
        console.log('Profil modificat');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }

  postProfil(): void {
    this.profilService.postProfil(this.profilPost).subscribe(
      () => {
        console.log('Profil creat');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }

  deleteProfil(id: number): void {
    this.profilService.deleteProfil(id).subscribe(
      () => {
        console.log('Profil sters');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }

  deleteProfilUtilizator(): void {
    this.profilService.deleteProfilUtilizator().subscribe(
      () => {
        console.log('Profil sters');
      },
      error => {
        console.log('Eroare', error);
      }
    );
  }
}
