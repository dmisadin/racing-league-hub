import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'noSpace'
})
export class NoSpacePipe implements PipeTransform {

	transform(value: any): string {
		return value.replace(/ /g, '');
	}

}
