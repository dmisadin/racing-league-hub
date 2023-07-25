import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonFormsComponent } from './season-forms.component';

describe('SeasonFormsComponent', () => {
  let component: SeasonFormsComponent;
  let fixture: ComponentFixture<SeasonFormsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeasonFormsComponent]
    });
    fixture = TestBed.createComponent(SeasonFormsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
