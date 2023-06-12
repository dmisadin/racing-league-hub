import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainResultComponent } from './main-result.component';

describe('MainResultComponent', () => {
  let component: MainResultComponent;
  let fixture: ComponentFixture<MainResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainResultComponent]
    });
    fixture = TestBed.createComponent(MainResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
