import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueAddEditComponent } from './league-add-edit.component';

describe('LeagueAddEditComponent', () => {
  let component: LeagueAddEditComponent;
  let fixture: ComponentFixture<LeagueAddEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LeagueAddEditComponent]
    });
    fixture = TestBed.createComponent(LeagueAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
