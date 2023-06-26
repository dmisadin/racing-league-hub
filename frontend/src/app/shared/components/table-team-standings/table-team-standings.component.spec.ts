import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableTeamStandingsComponent } from './table-team-standings.component';

describe('TableTeamStandingsComponent', () => {
  let component: TableTeamStandingsComponent;
  let fixture: ComponentFixture<TableTeamStandingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TableTeamStandingsComponent]
    });
    fixture = TestBed.createComponent(TableTeamStandingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
