import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'raceTime'
})
export class RaceTimePipe implements PipeTransform {

    transform(value: number): string {
        const miliseconds = value * 1000; // Convert to integer (miliseconds)
        let minutes = Math.floor(value / 60);
        const seconds = (miliseconds % 60000) / 1000;
        const hours = Math.floor(minutes / 60)
        minutes %= 60;
        let result = '';

        if (hours)
            result += hours + ':';
        if (minutes)
            result += ((minutes < 10 && hours) ? '0' : '') + minutes + ':';
        else
            result += (hours ? '00:' : '');

        result += ((seconds < 10 && minutes)? '0' : '') + seconds.toFixed(3);

        return result
    }
}
