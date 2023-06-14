import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultRowComponent } from './result-row.component';

describe('ResultRowComponent', () => {
  let component: ResultRowComponent;
  let fixture: ComponentFixture<ResultRowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ResultRowComponent]
    });
    fixture = TestBed.createComponent(ResultRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
