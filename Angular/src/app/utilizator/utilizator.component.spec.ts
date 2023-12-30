import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilizatorComponent } from './utilizator.component';

describe('UtilizatorComponent', () => {
  let component: UtilizatorComponent;
  let fixture: ComponentFixture<UtilizatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UtilizatorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UtilizatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
