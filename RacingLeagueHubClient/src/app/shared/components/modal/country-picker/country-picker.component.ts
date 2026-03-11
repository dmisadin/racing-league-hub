import { Component, computed, signal, output, effect } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgSelectComponent } from '@ng-select/ng-select';
import { CountryLean } from '../../../models/country';
import { HttpClient } from '@angular/common/http';
import countriesLean from '../../../../shared/datasets/countries-lean.json'

@Component({
    selector: 'country-picker',
    imports: [FormsModule, NgSelectComponent],
    templateUrl: './country-picker.component.html',
})
export class CountryPickerComponent {
    onSelected = output<CountryLean>();
    countries: CountryLean[] = countriesLean;

    isOpen = signal(false);
    selectedCountry = signal<CountryLean | null>(null);
    searchTerm = signal('');

    filteredCountries = computed(() => {
        const term = this.searchTerm().toLowerCase();
        if (term.length === 0)
            return this.countries;
        return this.countries.filter(c =>
            c.name.toLowerCase().includes(term)
        );
    });

    constructor(private http: HttpClient) {
        effect(() => {
            console.log('Selected country:', this.selectedCountry());
        });
    }

    select(country: CountryLean) {
        this.selectedCountry.set(country);
        this.onSelected.emit(country);
        this.resetSearch();
    }

    search(term: string) {
        this.searchTerm.set(term);
    }

    clear() {
        this.resetSearch();
        this.selectedCountry.set(null);
    }

    resetSearch() {
        this.searchTerm.set('');
    }
}
