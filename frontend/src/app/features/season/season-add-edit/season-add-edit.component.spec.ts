import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonAddEditComponent } from './season-add-edit.component';

describe('SeasonAddEditComponent', () => {
  let component: SeasonAddEditComponent;
  let fixture: ComponentFixture<SeasonAddEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeasonAddEditComponent]
    });
    fixture = TestBed.createComponent(SeasonAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
