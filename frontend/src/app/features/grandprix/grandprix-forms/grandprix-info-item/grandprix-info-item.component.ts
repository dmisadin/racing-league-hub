import { Component, Output, EventEmitter, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-grandprix-info-item',
  templateUrl: './grandprix-info-item.component.html',
  styleUrls: ['./grandprix-info-item.component.scss']
})
export class GrandprixInfoItemComponent {
  @Input() public grandPrixInfoItem!: FormGroup;
	@Output() public removeGrandPrixInfoItemEvent: EventEmitter<number> = new EventEmitter<number>();
	@Input() arrayIndex: number = 0;
  timeForm!: FormGroup;
	faTrashCan = faTrashCan;
	
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.timeForm = this.fb.group({
      dateTime: ['', Validators.required],
      timezone: ['+01:00']
    });

    this.timeForm.get('dateTime')?.valueChanges.subscribe(() => {
      this.onDateTimeChange();
    });

    this.timeForm.get('timezone')?.valueChanges.subscribe(() => {
      this.onDateTimeChange();
    });
  }

	static addGrandPrixInfoItem(): FormGroup {
		return new FormGroup({
			name: new FormControl("Bahrain Grand Prix", [Validators.required, Validators.maxLength(255)]),
			startTime: new FormControl("", Validators.required),
			hasSprint: new FormControl(false, Validators.required),
			youTubeURL: new FormControl("youtube.com", Validators.maxLength(255)),
			trackId: new FormControl(0, Validators.required),
		});
	}
	public removeGrandPrixInfoItem(index: number): void {
		this.removeGrandPrixInfoItemEvent.next(index);
	}

  onDateTimeChange() {
    const dateTime = this.timeForm.get('dateTime')?.value;
    const timezone = this.timeForm.get('timezone')?.value;
    const startTime = dateTime + timezone;
    this.grandPrixInfoItem.patchValue({ startTime: startTime });
  }
  
  mockTracks = [
    {id: 0, name: "Albert Park Circuit"},
    {id: 1, name: "Circuit Paul Ricard"},
    {id: 2, name: "Shanghai International Circuit"},
    {id: 3, name: "Bahrain International Circuit"},
  ]

  mockCountries = [
    {id: 1, nameEnglish: "Croatia"},
    {id: 2, nameEnglish: "Serbia"},
    {id: 3, nameEnglish: "Slovenia"},
    {id: 4, nameEnglish: "Bosnia and Herzegovina"},
  ]

  timezone = [
    "+00:00",
    "+01:00",
    "+02:00",
    "+03:00",
    "+04:00",
    "+05:00",
    "+06:00",
    "+07:00",
    "+08:00",
    "+09:00",
    "+10:00",
    "+11:00",
    "+12:00",
    "-01:00",
    "-02:00",
    "-03:00",
    "-04:00",
    "-05:00",
    "-06:00",
    "-07:00",
    "-08:00",
    "-09:00",
    "-10:00",
    "-11:00",
    "-12:00",
  ]

}
