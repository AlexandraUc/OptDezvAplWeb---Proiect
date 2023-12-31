import { TestBed } from '@angular/core/testing';

import { SchimbaParolaService } from './schimba-parola.service';

describe('SchimbaParolaService', () => {
  let service: SchimbaParolaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SchimbaParolaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
