import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RaceCardComponent } from './race-card.component';

describe('RaceCardComponent', () => {
  let component: RaceCardComponent;
  let fixture: ComponentFixture<RaceCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RaceCardComponent]
    });
    fixture = TestBed.createComponent(RaceCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
