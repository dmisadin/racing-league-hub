import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-league-add-edit',
  templateUrl: './league-add-edit.component.html',
  styleUrls: ['./league-add-edit.component.scss']
})
export class LeagueAddEditComponent {
  leagueForm = this.fb.group({
    
  })

  constructor(private fb: FormBuilder) {}
}
