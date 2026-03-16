import { Component, computed, signal, forwardRef, Injector, inject, OnInit } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, NgControl, ReactiveFormsModule } from '@angular/forms';
import { NgSelectComponent } from '@ng-select/ng-select';
import { CountryLean } from '../../../models/country';
import countriesLean from '../../../datasets/countries-lean.json'

@Component({
    selector: 'country-picker',
    imports: [FormsModule, NgSelectComponent],
    templateUrl: './country-picker.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => CountryPickerComponent),
            multi: true
        }
    ]
})
export class CountryPickerComponent implements ControlValueAccessor, OnInit {
    protected readonly injector = inject(Injector);
    protected control: NgControl | null = null;
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

    ngOnInit(): void {
        this.control = this.injector.get(NgControl, null);
    }

    onChange: (value: any) => void = () => { };
    onTouched: () => void = () => { };

    writeValue(value: string): void {
        const country = this.countries.find(c => c.alpha2 === value) || null;
        console.log(country)
        this.selectedCountry.set(country);
    }

    registerOnChange(fn: any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        // optional: handle disabled state
    }

    select(country?: CountryLean) {
        this.resetSearch();
        this.onTouched();

        if (!country) {
            this.selectedCountry.set(null);
            this.onChange(null); 
            return;
        }

        this.selectedCountry.set(country);
        this.onChange(country.alpha2); 
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
