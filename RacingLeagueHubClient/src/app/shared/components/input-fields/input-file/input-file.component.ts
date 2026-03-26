import { Component, forwardRef, inject, input, signal } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { ResourceService } from '../../../../core/services/resource.service';
import { ResourceDto } from '../../../models/resource';
import { BaseFormControl } from '../base-form-control';
import { FileSizePipe } from '../../../pipes/file-size.pipe';
import { IsImageExtensionPipe } from '../../../pipes/is-image-extension.pipe';

@Component({
    selector: 'input-file',
    templateUrl: './input-file.component.html',
    imports: [FileSizePipe, IsImageExtensionPipe],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => InputFileComponent),
            multi: true
        }
    ]
})
export class InputFileComponent extends BaseFormControl<number> {
    private readonly resourceService = inject(ResourceService);

    accept = input<string>('*/*');
    isThumbnail = input<boolean | null>(null);

    uploading = signal(false);
    uploadError = signal<string | null>(null);
    uploadedResource = signal<ResourceDto | null>(null);

    override writeValue(value: number | null): void {
        super.writeValue(value);
        
        if (!value) {
            this.uploadedResource.set(null);
            return;
        }

        this.resourceService.getById(value).subscribe({
            next: (resource) => this.uploadedResource.set(resource),
            error: () => this.uploadedResource.set(null)
        });
    }

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
                if (resource.id)
                    this.setValue(resource.id);
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