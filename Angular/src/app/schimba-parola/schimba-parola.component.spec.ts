import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchimbaParolaComponent } from './schimba-parola.component';

describe('SchimbaParolaComponent', () => {
  let component: SchimbaParolaComponent;
  let fixture: ComponentFixture<SchimbaParolaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SchimbaParolaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SchimbaParolaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
