import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonInfoFormComponent } from './season-info-form.component';

describe('SeasonInfoFormComponent', () => {
  let component: SeasonInfoFormComponent;
  let fixture: ComponentFixture<SeasonInfoFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeasonInfoFormComponent]
    });
    fixture = TestBed.createComponent(SeasonInfoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
