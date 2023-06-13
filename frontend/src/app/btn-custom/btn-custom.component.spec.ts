import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BtnCustomComponent } from './btn-custom.component';

describe('BtnCustomComponent', () => {
  let component: BtnCustomComponent;
  let fixture: ComponentFixture<BtnCustomComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BtnCustomComponent]
    });
    fixture = TestBed.createComponent(BtnCustomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
