import { Component, Input } from '@angular/core';
import { ControlContainer, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-switch',
  templateUrl: './switch.component.html',
  styleUrls: ['./switch.component.scss']
})
export class SwitchComponent {
  @Input() controlName: string = "";
  @Input() onOff: boolean = true;

  constructor(private parentControl: ControlContainer) {  }

  public parentFormGroup!: FormGroup;

  ngOnInit(): void {
    this.parentFormGroup = this.parentControl.control as FormGroup;
    this.parentFormGroup.registerControl(this.controlName, new FormControl(false));
  }

  get status() {
    return this.parentFormGroup.get(this.controlName)?.value;
  }
}
