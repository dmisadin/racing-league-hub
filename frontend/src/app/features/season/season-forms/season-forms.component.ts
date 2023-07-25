import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-season-forms',
  templateUrl: './season-forms.component.html',
  styleUrls: ['./season-forms.component.scss']
})
export class SeasonFormsComponent {
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;

  currentStep = 1;
  totalSteps = 8;
  preview = '';
  isSubmitted: boolean = false;
  stepFormNames = ["info", "qualPoints", "sprintPoints", "racePoints", "fastestLapPoints", "lobbySettings", "assists"]
  infoValue = { name: "", game: "", platform: "", lapsRequiredPercentage: 90 };

  qualPointsArray = this.fb.array([]);
  sprintPointsArray = this.fb.array([]);
  racePointsArray = this.fb.array([]);
  fastestLapPointsArray = this.fb.array([]);

  seasonForm = this.fb.group({
    // The rest of form group is added inside child components with parent.addControl()
  });

  constructor(private fb: FormBuilder) { }

  onSubmit(): void {
    this.isSubmitted = true;
    console.log("Submitted form: ", this.seasonForm.value, this.seasonForm.valid, this.seasonForm);
    //Add redirect from form to created Season page.
  }

  nextStep() {
    if (this.currentStep < this.totalSteps)
      this.currentStep++;
  }

  previousStep() {
    if (this.currentStep > 1)
      this.currentStep--;
  }

  get stepValidity() {
    return this.seasonForm?.get(this.stepFormNames[this.currentStep - 1])?.valid;
    // This can be improved by checking if all the steps before it are valid as well.
    // But right now, you can't skip the steps, so it doesn't mater until something like nonlinear "Material Stepper" is implemented
  }

  getControl(name: string): FormControl {
    return this.seasonForm.get(name) as FormControl;
  }

  checkError(error: string, ...name: string[]) {
    return name.some(el => this.getControl(el)?.hasError(error));
  }
}
