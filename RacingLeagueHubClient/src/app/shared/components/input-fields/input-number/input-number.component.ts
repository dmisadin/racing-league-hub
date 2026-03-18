import { Component, forwardRef, input, Injector } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseFormControl } from '../base-form-control';

@Component({
    selector: 'input-number',
    templateUrl: './input-number.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => InputNumberComponent),
            multi: true
        }
    ]
})
export class InputNumberComponent extends BaseFormControl<number> {
    prefix = input<string | null>(null);
    suffix = input<string | null>(null);
    min = input<number | null>(null);
    max = input<number | null>(null);
    
    onInput(event: Event) {
        const input = event.target as HTMLInputElement;
        const value = input.value === '' ? null : Number(input.value);
        this.setValue(value);
    }
}