import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrandprixInfoItemComponent } from './grandprix-info-item.component';

describe('GrandprixInfoItemComponent', () => {
  let component: GrandprixInfoItemComponent;
  let fixture: ComponentFixture<GrandprixInfoItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GrandprixInfoItemComponent]
    });
    fixture = TestBed.createComponent(GrandprixInfoItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
