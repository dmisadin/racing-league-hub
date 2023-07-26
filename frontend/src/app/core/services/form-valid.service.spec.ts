import { TestBed } from '@angular/core/testing';

import { FormValidService } from './form-valid.service';

describe('FormValidService', () => {
  let service: FormValidService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormValidService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
