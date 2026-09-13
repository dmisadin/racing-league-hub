import { ChangeDetectionStrategy, Component, computed, forwardRef, input, signal } from '@angular/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { environment } from '../../../../../environments/environment';
import { httpResource } from '@angular/common/http';

export interface SelectOption {
    value: string | number;
    label: string;
}

@Component({
    selector: 'api-input-select',
    imports: [NgSelectModule, FormsModule],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => ApiInputSelectComponent),
            multi: true,
        },
    ],
    template: `
    <ng-select [items]="filteredOptions()"
        bindLabel="label"
        bindValue="value"
        [placeholder]="placeholder()"
        [loading]="optionsResource.isLoading()"
        [disabled]="isDisabled() || optionsResource.isLoading() || !!optionsResource.error()"
        [searchable]="true"
        [ngModel]="value()"
        (ngModelChange)="onValueChange($event)"
        (search)="onSearch($event)"
        (clear)="onClear()"
        (blur)="onTouched()">
        <ng-template ng-label-tmp let-item="item">
            {{ item.label }}
        </ng-template>
    
        <ng-template ng-option-tmp let-item="item">
            <span [innerHTML]="highlight(item.label, searchTerm())"></span>
        </ng-template>
 
        @if (optionsResource.error()) {
        <ng-template ng-notfound-tmp>
            <div class="ng-option disabled">Failed to load options</div>
        </ng-template>
        }
    </ng-select>
  `,
})
export class ApiInputSelectComponent implements ControlValueAccessor {
    readonly baseApiUrl = environment.apiUrl;
    endpoint = input.required<string>();
    labelKey = input<string>('label');
    valueKey = input<string>('id');
    placeholder = input<string>('Select an option...');

    // --- CVA state (signals so the template stays zoneless) ---
    readonly value = signal<string | number | null>(null);
    readonly isDisabled = signal<boolean>(false);
    readonly searchTerm = signal<string>('');

    // --- CVA callbacks (set by Angular's forms machinery) ---
    private onChange: (v: string | number | null) => void = () => { };
    onTouched: () => void = () => { };

    // --- Resource: fetches all options through Angular HttpClient ---
    readonly optionsResource = httpResource<Record<string, unknown>[]>(
        () => this.baseApiUrl + this.endpoint()
    );

    // --- Derived: map API response to SelectOption[] and apply local filtering ---
    readonly filteredOptions = computed<SelectOption[]>(() => {
        const data = this.optionsResource.value() ?? [];

        const allOptions = data.map((item) => ({
            label: String(item[this.labelKey()]),
            value: item[this.valueKey()] as string | number,
        }));

        const term = this.searchTerm().toLowerCase().trim();

        if (!term) {
            return allOptions;
        }

        return allOptions.filter((opt) =>
            opt.label.toLowerCase().includes(term)
        );
    });


    writeValue(value: string | number | null): void {
        this.value.set(value ?? null);
    }
    registerOnChange(fn: (v: string | number | null) => void): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: () => void): void {
        this.onTouched = fn;
    }

    setDisabledState(isDisabled: boolean): void {
        this.isDisabled.set(isDisabled);
    }


    onValueChange(value: string | number | null): void {
        this.value.set(value);
        this.onChange(value);
    }

    onSearch(event: { term: string }): void {
        this.searchTerm.set(event.term);
    }

    onClear(): void {
        this.searchTerm.set('');
        this.onChange(null);
    }

    highlight(label: string, term: string): string {
        if (!term) return label;
        const escaped = term.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
        return label.replace(new RegExp(`(${escaped})`, 'gi'), '<mark>$1</mark>');
    }
}