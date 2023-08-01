import { Component, DoCheck } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuthService } from 'app/core/services/auth.service';
import { RegisterUser } from 'app/shared/models/user/RegisterUser';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements DoCheck {

  registerUser: RegisterUser = new RegisterUser;

  //Find a solution to replace this.fb.group() which is deprecated
  registerForm = this.fb.group({
    username: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  }, {validator: this.checkPasswords})

  constructor(private fb: FormBuilder, private authService: AuthService) { }

  ngDoCheck(): void {
    // if(this.registerForm.value.password != this.registerForm.value.confirmPassword)
    // {console.log("Password doesnt match")}
    // else console.log("Password matches")
  }

  onSubmit(): void {
    console.log("Submitted form: ", this.registerForm.value, this.registerForm.valid);
    this.registerUser = {
      username: this.registerForm.value.username,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password
    }

    if(this.registerForm.valid)
      this.register(this.registerUser);
  }

  /**Validator for checking if two password inputs are the same.*/
  checkPasswords(group: FormGroup) {
    let pass = group.controls['password'].value;
    let confirmPass = group.controls['confirmPassword'].value;

    return pass === confirmPass ? null : { notSame: true }
  }

  async register(registerUser: RegisterUser): Promise<void>{
    const result = await firstValueFrom(this.authService.register(registerUser));
    console.log(result);
  }
}
