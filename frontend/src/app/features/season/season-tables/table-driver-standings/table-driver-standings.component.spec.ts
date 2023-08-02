import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableDriverStandingsComponent } from './table-driver-standings.component';

describe('TableDriverStandingsComponent', () => {
  let component: TableDriverStandingsComponent;
  let fixture: ComponentFixture<TableDriverStandingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TableDriverStandingsComponent]
    });
    fixture = TestBed.createComponent(TableDriverStandingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
