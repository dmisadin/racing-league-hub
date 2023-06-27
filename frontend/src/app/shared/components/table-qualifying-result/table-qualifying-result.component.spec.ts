import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableQualifyingResultComponent } from './table-qualifying-result.component';

describe('TableQualifyingResultComponent', () => {
  let component: TableQualifyingResultComponent;
  let fixture: ComponentFixture<TableQualifyingResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TableQualifyingResultComponent]
    });
    fixture = TestBed.createComponent(TableQualifyingResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
