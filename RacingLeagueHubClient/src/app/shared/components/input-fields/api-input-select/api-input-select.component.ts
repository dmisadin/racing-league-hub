import {
    ChangeDetectionStrategy,
    Component,
    computed,
    input,
    output,
    resource,
    signal,
} from '@angular/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../../../environments/environment';

export interface SelectOption {
    value: string | number;
    label: string;
}

@Component({
    selector: 'api-input-select',
    standalone: true,
    imports: [NgSelectModule, FormsModule],
    changeDetection: ChangeDetectionStrategy.OnPush,
    template: `
    <ng-select
      [items]="filteredOptions()"
      bindLabel="label"
      bindValue="value"
      [placeholder]="placeholder()"
      [loading]="optionsResource.isLoading()"
      [disabled]="optionsResource.isLoading() || !!optionsResource.error()"
      [searchable]="true"
      [(ngModel)]="selected"
      (ngModelChange)="selectedChange.emit($event)"
      (search)="onSearch($event)"
      (clear)="onClear()"
    >
      <ng-template ng-label-tmp let-item="item">
        {{ item.label }}
      </ng-template>

      <ng-template ng-option-tmp let-item="item" let-search="searchTerm">
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
export class ApiInputSelectComponent {
    // --- Inputs ---
    /** URL to fetch options from. Expected JSON: SelectOption[] or any[] with labelKey/valueKey */
    readonly baseApiUrl = environment.apiUrl;
    endpoint = input.required<string>();
    /** Key to use as the label. Defaults to 'label' */
    labelKey = input<string>('label');
    /** Key to use as the value. Defaults to 'id' */
    valueKey = input<string>('id');
    placeholder = input<string>('Select an option...');

    // --- Outputs ---
    selectedChange = output<string>();

    // --- Internal state ---
    selected: string | number | null = null;
    readonly searchTerm = signal<string>('');

    // --- Resource: loads all options once ---
    readonly optionsResource = resource({
        // Re-fetches automatically if endpoint input signal changes
        params: () => ({ url: this.baseApiUrl + this.endpoint() }),
        loader: async ({ params }): Promise<SelectOption[]> => {
            const response = await fetch(params.url);
            if (!response.ok) {
                throw new Error(`HTTP ${response.status}: ${response.statusText}`);
            }
            const data: Record<string, unknown>[] = await response.json();
            // Map raw API data to SelectOption shape using configured keys
            return data.map((item) => ({
                label: String(item[this.labelKey()]),
                value: item[this.valueKey()] as string | number,
            }));
        },
    });

    // --- Derived: filter locally by search term (purely computed, no local mutation) ---
    readonly filteredOptions = computed<SelectOption[]>(() => {
        const all = this.optionsResource.value() ?? [];
        const term = this.searchTerm().toLowerCase().trim();
        if (!term) return all;
        return all.filter((opt) => opt.label.toLowerCase().includes(term));
    });

    // --- Handlers ---
    onSearch(event: { term: string }): void {
        this.searchTerm.set(event.term);
    }

    onClear(): void {
        this.searchTerm.set('');
    }

    /** Wraps matching substring in <mark> for highlight rendering */
    highlight(label: string, term: string): string {
        if (!term) return label;
        const escaped = term.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
        return label.replace(
            new RegExp(`(${escaped})`, 'gi'),
            '<mark>$1</mark>',
        );
    }
}