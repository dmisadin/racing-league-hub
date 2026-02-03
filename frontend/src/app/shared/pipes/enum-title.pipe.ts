import { Pipe, PipeTransform } from '@angular/core';
import { IEnum } from '../models/interfaces';

@Pipe({
    name: 'enumTitle'
})
export class EnumTitlePipe implements PipeTransform {

    transform(value: number, enumerable: IEnum): string {
        var key: string = Object.keys(enumerable).find(key => enumerable[key].Value === value) ?? "";
        return enumerable[key].Title ?? "";
    }
}
