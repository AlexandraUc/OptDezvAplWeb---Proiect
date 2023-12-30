import { TestBed } from '@angular/core/testing';

import { ArticolService } from './articol.service';

describe('ArticolService', () => {
  let service: ArticolService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ArticolService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
