import { Component, OnInit, Output, EventEmitter, ChangeDetectionStrategy, Input } from '@angular/core';
import { ControlContainer, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-season-info-form',
  templateUrl: './season-info-form.component.html',
  styleUrls: ['./season-info-form.component.scss'],
})



export class SeasonInfoFormComponent implements OnInit {
  @Input() infoValues = { name: "", game: "F122", platform: "PC", lapsRequiredPercentage: 90 };
  @Output() infoValuesChange = new EventEmitter();

  constructor(private fb: FormBuilder, private parentControl: ControlContainer) { }

  parentFormGroup = this.parentControl.control as FormGroup;

  infoFormGroup = this.fb.group({
    name: [this.infoValues.name, Validators.required],
    game: [this.infoValues.game, Validators.required],
    platform: [this.infoValues.platform, Validators.required],
    lapsRequiredPercentage: [this.infoValues.lapsRequiredPercentage, Validators.required],
  });

  ngOnInit(): void {
    console.log(this.infoFormGroup)
    this.parentFormGroup.addControl('info', this.infoFormGroup);

    this.infoValuesChange.emit(this.infoFormGroup.value);
  }

  sendDataToParent() {
    console.log("child ", this.infoFormGroup.value);
    this.infoValuesChange.emit(this.infoFormGroup.value);
  }

  get nameField(): FormControl {
    return this.infoFormGroup?.get('name') as FormControl;
  }
  get lapsField(): FormControl {
    return this.infoFormGroup?.get('lapsRequiredPercentage') as FormControl;
  }
}