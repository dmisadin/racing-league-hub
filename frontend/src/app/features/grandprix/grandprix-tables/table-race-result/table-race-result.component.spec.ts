import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableRaceResultComponent } from './table-race-result.component';

describe('TableRaceResultComponent', () => {
  let component: TableRaceResultComponent;
  let fixture: ComponentFixture<TableRaceResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TableRaceResultComponent]
    });
    fixture = TestBed.createComponent(TableRaceResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
