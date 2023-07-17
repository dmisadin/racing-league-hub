import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';


import { FormValidService } from 'app/core/services/form-valid.service';
import { Subscription } from 'rxjs';

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

  infoValue = { name: "", game: "", platform: "", lapsRequiredPercentage: 90 };
  qualPointsArray = this.fb.array([]);

  seasonForm = this.fb.group({
    
  })

  constructor(private fb: FormBuilder, private cdr: ChangeDetectorRef) { }
  ngOnInit() {
    console.log(this.currentStep, this.seasonForm.value);
  }
  ngOnChanges() {
    
    console.log("on changes parent ", this.seasonForm.controls)
  }
  ngAfterViewInit() {
    this.cdr.detectChanges();
    console.log("after view init ", this.seasonForm.controls)
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
