import { Component, forwardRef, input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseFormControl } from '../base-form-control';

@Component({
    selector: 'input-text',
    templateUrl: './input-text.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => InputTextComponent),
            multi: true
        }
    ]
})
export class InputTextComponent extends BaseFormControl<string> {
    prefix = input<string | null>(null);
    suffix = input<string | null>(null);
    type = input<TextInputType>('text');

    onInput(event: Event) {
        const input = event.target as HTMLInputElement;
        this.setValue(input.value);
    }
}

export type TextInputType = 'text' | 'email' | 'password' | 'url';