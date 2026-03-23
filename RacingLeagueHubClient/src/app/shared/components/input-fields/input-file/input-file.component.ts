import { Component, forwardRef, inject, input, signal } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { ResourceService } from '../../../../core/services/resource.service';
import { Resource } from '../../../models/resource';
import { BaseFormControl } from '../base-form-control';
import { FileSizePipe } from '../../../pipes/file-size.pipe';
import { IsImagePipe } from "../../../pipes/is-image.pipe";

@Component({
    selector: 'input-file',
    templateUrl: './input-file.component.html',
    imports: [FileSizePipe, IsImagePipe],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => InputFileComponent),
            multi: true
        }
    ]
})
export class InputFileComponent extends BaseFormControl<string> {
    // Inputs
    accept = input<string>('*/*');
    isThumbnail = input<boolean | null>(null);

    // Internal state
    uploading = signal(false);
    uploadError = signal<string | null>(null);
    uploadedResource = signal<Resource | null>(null);

    private readonly resourceService = inject(ResourceService);

    // ControlValueAccessor writes the resource uid as the form value
    // So validators like Validators.required work naturally on the uid string
    onInput(_event: Event): void {
        // not used for file input — see onFileSelected
    }

    onFileSelected(event: Event): void {
        const input = event.target as HTMLInputElement;
        const file = input.files?.[0];
        if (!file) return;

        this.uploading.set(true);
        this.uploadError.set(null);
        this.onTouched();

        this.resourceService.upload(file, this.isThumbnail() ?? undefined).subscribe({
            next: (resource) => {
                this.uploading.set(false);
                this.uploadedResource.set(resource);
                this.setValue(resource.uid);  // sets form value to uid string
            },
            error: () => {
                this.uploading.set(false);
                this.uploadError.set('Upload failed. Please try again.');
                this.setValue(null);
            }
        });
    }

    onClear(): void {
        this.uploadedResource.set(null);
        this.uploadError.set(null);
        this.setValue(null);
    }
}