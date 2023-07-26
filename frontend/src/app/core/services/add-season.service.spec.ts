import { TestBed } from '@angular/core/testing';

import { AddSeasonService } from './add-season.service';

describe('AddSeasonService', () => {
  let service: AddSeasonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddSeasonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
