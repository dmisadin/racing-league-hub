import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'mediumdateToMonthyear'
})

/** Convert MONT DAY, YEAR to MONT YEAR*/
export class MediumdateToMonthyearPipe implements PipeTransform {

  transform(mediumDate: string): string {
    // Convert mediumDate to local datetime format (YYYY-MM-DDTHH:mm)
    return mediumDate.replace(/(\d\d,)/g, '');
  }
}
