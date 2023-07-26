import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';

@Component({
	selector: 'app-points-item',
	templateUrl: './points-item.component.html',
	styleUrls: ['./points-item.component.scss']
})
export class PointsItemComponent {
	@Input() public pointsItem!: FormGroup;
	@Output() public removePointsItemEvent: EventEmitter<number> = new EventEmitter<number>();
	@Input() arrayIndex: number = 0;
	faTrashCan = faTrashCan;
	
	constructor() { }


	static addPointsItem(position: number, points: number): FormGroup {
		return new FormGroup({
			position: new FormControl(position, [Validators.required, Validators.min(1), Validators.max(22)]),
			points: new FormControl(points, [Validators.required, Validators.min(1)])
		});

	}

	public removePointsItem(index: number): void {
		this.removePointsItemEvent.next(index);
	}
}
