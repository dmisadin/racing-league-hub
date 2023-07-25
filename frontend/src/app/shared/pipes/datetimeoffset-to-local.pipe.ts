import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'datetimeoffsetToLocal'
})
export class DatetimeoffsetToLocalPipe implements PipeTransform {

  transform(datetimeoffset: string): string {
    // Convert DateTimeOffset to local datetime format (YYYY-MM-DDTHH:mm)
    const date = new Date(datetimeoffset);
    return date.toISOString().slice(0, 16);
  }
}
