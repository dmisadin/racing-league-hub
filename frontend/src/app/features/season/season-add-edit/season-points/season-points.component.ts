import { Component, Output, EventEmitter, Input } from '@angular/core';
import { AbstractControl, ControlContainer, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { PointsItemComponent } from '../points-item/points-item.component';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

type SessionName = "Qualifying" | "Sprint" | "Race";

@Component({
  selector: 'app-season-points',
  templateUrl: './season-points.component.html',
  styleUrls: ['./season-points.component.scss']
})
export class SeasonPointsComponent {
  faPlus = faPlus;
  @Input() sessionName: SessionName = "Qualifying";
  @Input() pointsArray!: FormArray;
  @Output() pointsArrayChange = new EventEmitter();

  sessionType = {
    Qualifying: "qualPoints",
    Sprint: "sprintPoints",
    Race: "racePoints"
  };
  constructor(private fb: FormBuilder, private parentControl: ControlContainer) { }

  parentFormGroup = this.parentControl.control as FormGroup;


  ngOnInit(): void {
    this.generatePointsForm();
    this.parentFormGroup.addControl(this.sessionType[this.sessionName], this.pointsArray);
  }

  generatePointsForm(): void {
  }

  public addPointsItem(): void {
    if (this.pointsArray.controls.length < 20){
      this.pointsArray.push(PointsItemComponent.addPointsItem(this.pointsArray.controls.length + 1, 0));
      console.log("child controls", this.pointsArray.controls)
    }
    else
      console.log("Maximum is 20 players"); // F2 lobbies support up to 22, check this later
  }

  public removePointsItem(index: number): void {
    this.pointsArray?.removeAt(index);
  }

  toFormGroup(a: AbstractControl) {
    return a as FormGroup;
  }
}
