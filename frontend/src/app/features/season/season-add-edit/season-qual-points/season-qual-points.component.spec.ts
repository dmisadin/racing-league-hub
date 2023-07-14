import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonQualPointsComponent } from './season-qual-points.component';

describe('SeasonQualPointsComponent', () => {
  let component: SeasonQualPointsComponent;
  let fixture: ComponentFixture<SeasonQualPointsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeasonQualPointsComponent]
    });
    fixture = TestBed.createComponent(SeasonQualPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
