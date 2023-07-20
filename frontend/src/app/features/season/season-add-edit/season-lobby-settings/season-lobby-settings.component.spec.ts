import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonLobbySettingsComponent } from './season-lobby-settings.component';

describe('SeasonLobbySettingsComponent', () => {
  let component: SeasonLobbySettingsComponent;
  let fixture: ComponentFixture<SeasonLobbySettingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeasonLobbySettingsComponent]
    });
    fixture = TestBed.createComponent(SeasonLobbySettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
