import { Component, DoCheck } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements DoCheck {
  registerForm = this.fb.group({
    username: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  }, {validator: this.checkPasswords})

  constructor(private fb: FormBuilder) { }

  ngDoCheck(): void {
    if(this.registerForm.value.password != this.registerForm.value.confirmPassword)
    {console.log("Password doesnt match")}
    else console.log("Password matches")
  }

  onSubmit(): void {
    console.log("Submitted form: ", this.registerForm.value, this.registerForm.valid);
  }

  /**Validator for checking if two password inputs are the same.*/
  checkPasswords(group: FormGroup) {
    let pass = group.controls['password'].value;
    let confirmPass = group.controls['confirmPassword'].value;

    return pass === confirmPass ? null : { notSame: true }
  }
}
