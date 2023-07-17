import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PointsItemComponent } from './points-item.component';

describe('PointsItemComponent', () => {
  let component: PointsItemComponent;
  let fixture: ComponentFixture<PointsItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PointsItemComponent]
    });
    fixture = TestBed.createComponent(PointsItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
