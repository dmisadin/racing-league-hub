import { Pipe, PipeTransform } from '@angular/core';
import { IEnum } from '../models/interfaces';

@Pipe({
    name: 'enumValue'
})
export class EnumValuePipe implements PipeTransform {

    transform(key: string, enumerable: IEnum): number {
        return enumerable[key].Value ?? 0;
    }

}
