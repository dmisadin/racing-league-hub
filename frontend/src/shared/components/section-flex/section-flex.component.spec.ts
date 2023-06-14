import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SectionFlexComponent } from './section-flex.component';

describe('SectionFlexComponent', () => {
  let component: SectionFlexComponent;
  let fixture: ComponentFixture<SectionFlexComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SectionFlexComponent]
    });
    fixture = TestBed.createComponent(SectionFlexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
