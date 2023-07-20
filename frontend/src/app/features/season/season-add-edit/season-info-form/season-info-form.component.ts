import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ControlContainer, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-season-info-form',
  templateUrl: './season-info-form.component.html',
  styleUrls: ['./season-info-form.component.scss'],
})


export class SeasonInfoFormComponent implements OnInit {
  @Input() infoValue = { name: "Sezona 5", game: "F122", platform: "PC", lapsRequiredPercentage: 90 };
  @Output() infoValueChange = new EventEmitter();

  constructor(private fb: FormBuilder, private parentControl: ControlContainer) { }

  parentFormGroup = this.parentControl.control as FormGroup;

  infoFormGroup = this.fb.group({
    name: [this.infoValue.name, Validators.required],
    game: [this.infoValue.game, Validators.required],
    platform: [this.infoValue.platform, Validators.required],
    lapsRequiredPercentage: [this.infoValue.lapsRequiredPercentage, [Validators.required, Validators.min(0), Validators.max(100)]],
  });

  ngOnInit(): void {
    console.log("infovalue on init", this.infoValue);
    this.parentFormGroup.addControl('info', this.infoFormGroup);
  }

  ngOnChanges() {
    console.log("infovalue on change", this.infoValue);

  }
  get nameField(): FormControl {
    return this.infoFormGroup?.get('name') as FormControl;
  }
  get lapsField(): FormControl {
    return this.infoFormGroup?.get('lapsRequiredPercentage') as FormControl;
  }
}