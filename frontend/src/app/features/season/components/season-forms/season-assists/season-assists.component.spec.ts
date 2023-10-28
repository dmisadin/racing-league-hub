import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonAssistsComponent } from './season-assists.component';

describe('SeasonAssistsComponent', () => {
  let component: SeasonAssistsComponent;
  let fixture: ComponentFixture<SeasonAssistsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeasonAssistsComponent]
    });
    fixture = TestBed.createComponent(SeasonAssistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
