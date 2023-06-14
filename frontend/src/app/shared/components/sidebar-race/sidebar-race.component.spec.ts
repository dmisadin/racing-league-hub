import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarRaceComponent } from './sidebar-race.component';

describe('SidebarRaceComponent', () => {
  let component: SidebarRaceComponent;
  let fixture: ComponentFixture<SidebarRaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SidebarRaceComponent]
    });
    fixture = TestBed.createComponent(SidebarRaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
