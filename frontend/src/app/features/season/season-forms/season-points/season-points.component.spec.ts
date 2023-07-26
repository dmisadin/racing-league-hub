import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonPointsComponent } from './season-points.component';

describe('SeasonPointsComponent', () => {
  let component: SeasonPointsComponent;
  let fixture: ComponentFixture<SeasonPointsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeasonPointsComponent]
    });
    fixture = TestBed.createComponent(SeasonPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
