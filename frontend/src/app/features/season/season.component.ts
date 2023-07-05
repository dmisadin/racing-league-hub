import { Component } from '@angular/core';

@Component({
  selector: 'app-season',
  templateUrl: './season.component.html',
  styleUrls: ['./season.component.scss'],
})
export class SeasonComponent {
  mockPoints = [15, 12, 10, 8, 6, 5, 4, 3, 2, 1];

  mockAssists = [
    {
      iconPath: 'assists/tractioncontrol.png',
      name: 'Traction Control',
      value: 'Medium',
    },
    { iconPath: 'assists/abs.png', name: 'ABS', value: 'Off' },
    {
      iconPath: 'assists/racingline.png',
      name: 'Racing Line',
      value: 'Corners only',
    },
    { iconPath: 'assists/gearbox.png', name: 'gearbox', value: 'Manual' },
  ];

  mockLobby = [
    { iconPath: 'lobby/qualifying.png', name: 'Qualifying', value: 'Short' },
    { iconPath: 'lobby/formationlap.png', name: 'Formation Lap', value: 'On' },
    { iconPath: 'lobby/race.png', name: 'Race Distance', value: '50%' },
    { iconPath: 'lobby/weather.png', name: 'Weather', value: 'Dynamic' },
    {
      iconPath: 'lobby/cornercutting.png',
      name: 'Corner cutting',
      value: 'strict',
    },
    { iconPath: 'lobby/damage.png', name: 'Damage', value: 'Standard' },
    { iconPath: 'lobby/parcferme.png', name: 'Parc Ferme', value: 'On' },
    { iconPath: 'lobby/engine.png', name: 'Engine', value: 'Equal' },
    { iconPath: 'lobby/safetycar.png', name: 'Safety Car', value: 'Reduced' },
    { iconPath: 'lobby/start.png', name: 'Starts', value: 'Manual' },
    { iconPath: 'lobby/collision.png', name: 'Collisions', value: 'On' },
    { iconPath: 'lobby/ghosting.png', name: 'Ghosting', value: 'Off' },
  ];
}
