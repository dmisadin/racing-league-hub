import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StandingsTablesComponent } from './standings-tables.component';

describe('StandingsTablesComponent', () => {
  let component: StandingsTablesComponent;
  let fixture: ComponentFixture<StandingsTablesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StandingsTablesComponent]
    });
    fixture = TestBed.createComponent(StandingsTablesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
