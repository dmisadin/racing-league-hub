import { inject, Pipe, PipeTransform } from '@angular/core';
import { CountryService } from '../../core/services/country.service';

@Pipe({
    name: 'flagUrl',
})
export class FlagUrlPipe implements PipeTransform {
    private countryService = inject(CountryService);
    
    transform(value: string): unknown {
        return this.countryService.getFlagUrl(value);
    }
}
