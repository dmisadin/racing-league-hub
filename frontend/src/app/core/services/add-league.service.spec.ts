import { TestBed } from '@angular/core/testing';

import { AddLeagueService } from './add-league.service';

describe('AddLeagueService', () => {
  let service: AddLeagueService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddLeagueService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
