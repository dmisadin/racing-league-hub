import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-season-add-edit',
  templateUrl: './season-add-edit.component.html',
  styleUrls: ['./season-add-edit.component.scss']
})
export class SeasonAddEditComponent {
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;

  currentStep = 1;
  totalSteps = 3;
  preview = '';
  isSubmitted: boolean = false;

  infoValues = { name: "", game: "", platform: "", lapsRequiredPercentage: 90};

  seasonForm = this.fb.group({
    test: ['', Validators.required]
  })

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    console.log(this.currentStep, this.seasonForm.value);
  
  }

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
  
  get infoValid() {
      return this.seasonForm?.get('info');
  }
}
