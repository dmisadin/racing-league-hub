import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RaceRowComponent } from './race-row.component';

describe('RaceRowComponent', () => {
  let component: RaceRowComponent;
  let fixture: ComponentFixture<RaceRowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RaceRowComponent]
    });
    fixture = TestBed.createComponent(RaceRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
