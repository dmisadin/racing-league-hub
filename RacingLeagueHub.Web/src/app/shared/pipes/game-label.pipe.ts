import { Pipe, PipeTransform } from '@angular/core';
import { Game } from '../models/enums';

@Pipe({
    name: 'gameLabel',
})
export class GameLabelPipe implements PipeTransform {
    transform(value: Game | null | undefined): string {
        if (value == null)
            return '';

        const name = Game[value];

        return name ? name.replace(/^F1(\d+)$/, 'F1 $1') : '';
    }
}
