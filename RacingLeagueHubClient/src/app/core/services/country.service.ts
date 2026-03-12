import { Injectable } from '@angular/core';
import { CountryLean } from '../../shared/models/country';
import countriesLean from '../../shared/datasets/countries-lean.json'

@Injectable({
    providedIn: 'root'
})
export class CountryService {
    private countriesLean: CountryLean[] = countriesLean;

    getCountryByAlpha2(code: string): CountryLean | null {
        return this.countriesLean.find(c => c.alpha2 === code) || null;
    }

    getFlagUrl(alpha2: string): string {
        return `images/flags/${alpha2.toLowerCase()}.svg`;
    }
}