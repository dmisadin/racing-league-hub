import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainResultsComponent } from './main-results.component';

describe('MainResultsComponent', () => {
  let component: MainResultsComponent;
  let fixture: ComponentFixture<MainResultsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainResultsComponent]
    });
    fixture = TestBed.createComponent(MainResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
