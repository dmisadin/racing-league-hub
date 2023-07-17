import { Component, OnInit, Output, EventEmitter, ChangeDetectionStrategy, Input } from '@angular/core';
import { AbstractControl, ControlContainer, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PointsItemComponent } from '../points-item/points-item.component';
import { faPlus } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-season-qual-points',
  templateUrl: './season-qual-points.component.html',
  styleUrls: ['./season-qual-points.component.scss']
})
export class SeasonQualPointsComponent {
  faPlus = faPlus;
  @Input() qualPointsArray!: FormArray;
  @Output() qualPointsArrayChange = new EventEmitter();

  constructor(private fb: FormBuilder, private parentControl: ControlContainer) { }

  parentFormGroup = this.parentControl.control as FormGroup;

  public qualPointsFormArray!: FormArray;

  get pointsArray(): FormArray {
    return this.qualPointsFormArray;
  }

  toFormGroup(a: AbstractControl) {
    return a as FormGroup;
  }

  ngOnInit(): void {
    this.generatePointsForm();
    console.log(this.qualPointsFormArray)
    this.parentFormGroup.addControl('qualPoints', this.qualPointsFormArray);
    this.qualPointsArrayChange.emit(this.qualPointsFormArray);
  }

  sendDataToParent() {
    this.qualPointsArrayChange.emit(this.qualPointsFormArray);
  }

  generatePointsForm(): void {
    console.log("input ", this.qualPointsArray)
    console.log("form ", this.qualPointsFormArray)
    if (this.qualPointsArray?.controls?.length) {
      console.log("usao")
      this.qualPointsFormArray = this.qualPointsArray;
    }

    else {
      this.qualPointsFormArray = this.fb.array([]);
    }
  }

  public addPointsItem(): void {
    if (this.pointsArray.controls.length < 20)
      this.pointsArray?.push(PointsItemComponent.addPointsItem(this.pointsArray.controls.length + 1, 0));
    else
      console.log("Maximum is 20 players"); // F2 lobbies support up to 22, check this later
  }

  public removePointsItem(index: number): void {
    this.pointsArray?.removeAt(index);
  }
}
