import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrandprixFormsComponent } from './grandprix-forms.component';

describe('GrandprixFormsComponent', () => {
  let component: GrandprixFormsComponent;
  let fixture: ComponentFixture<GrandprixFormsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GrandprixFormsComponent]
    });
    fixture = TestBed.createComponent(GrandprixFormsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
