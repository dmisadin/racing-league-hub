import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'dateDiffFromNow'
})
export class DateDiffFromNowPipe implements PipeTransform {

    transform(date: Date | string): string {
        let currentDate = new Date();
        date = new Date(date);

        let time = (currentDate.getTime() - date.getTime()) / 1000;
        const prefix = time < 0 ? -1 : 1;
        time = Math.abs(time);
        //seconds
        if (time < 60)
            return time + " s";
        //minutes
        time = Math.floor(time / 60);
        if (time < 60)
            return time + " m";
        //hours
        time = Math.floor(time / 60);
        if (time < 24)
            return time + " h";
        //days
        time = Math.floor(time / 24);
        if (time <= 366)
            return time + (time <= 1 ? " day" : " days");
        //years
        time = Math.floor(time / 365);
        return time + " year";
    }

}
