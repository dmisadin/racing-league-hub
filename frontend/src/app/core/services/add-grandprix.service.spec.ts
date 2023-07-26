import { TestBed } from '@angular/core/testing';

import { AddGrandprixService } from './add-grandprix.service';

describe('AddGrandprixService', () => {
  let service: AddGrandprixService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddGrandprixService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
