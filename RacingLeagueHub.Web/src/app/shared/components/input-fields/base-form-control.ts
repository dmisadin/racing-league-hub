import { Directive, inject, Injector, input, OnInit } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Directive()
export abstract class BaseFormControl<T> implements ControlValueAccessor, OnInit {
    protected readonly injector = inject(Injector);

    label = input("");
    placeholder = input<string>("");
    customValidatorName = input<string | null>();
    value: T | null = null;
    disabled = false;

    control: NgControl | null = null;

    protected onChange: (value: T | null) => void = () => { };
    protected onTouched: () => void = () => { };

    ngOnInit() {
        this.control = this.injector.get(NgControl, null);
    }

    writeValue(value: T | null): void {
        this.value = value;
    }

    registerOnChange(fn: (value: T | null) => void): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: () => void): void {
        this.onTouched = fn;
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    protected setValue(value: T | null) {
        this.value = value;
        this.onChange(value);
    }

    abstract onInput(event: Event): void;

    get invalid() {
        return this.control?.invalid && (this.control?.touched || this.control?.dirty);
    }

    get errors() {
        return this.control?.errors ?? null;
    }
}