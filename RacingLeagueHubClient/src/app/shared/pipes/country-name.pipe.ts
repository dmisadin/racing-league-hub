import { inject, Pipe, PipeTransform } from '@angular/core';
import { CountryService } from '../../core/services/country.service';

@Pipe({
    name: 'countryName',
})
export class CountryNamePipe implements PipeTransform {
    private countryService = inject(CountryService);
    
    transform(alpha2Code?: string): unknown {
        if (!alpha2Code) return "";
        const country = this.countryService.getCountryByAlpha2(alpha2Code);
        if (!country) return "";
        return country.name;
    }
}
