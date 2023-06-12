import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainBtnComponent } from './main-btn.component';

describe('MainBtnComponent', () => {
  let component: MainBtnComponent;
  let fixture: ComponentFixture<MainBtnComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainBtnComponent]
    });
    fixture = TestBed.createComponent(MainBtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
