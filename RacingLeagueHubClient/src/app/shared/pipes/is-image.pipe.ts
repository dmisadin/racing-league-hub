import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'isImage',
    standalone: true,
    pure: true
})
export class IsImagePipe implements PipeTransform {
    transform(mimeType?: string): boolean {
        if (!mimeType) return false;
        return mimeType.startsWith('image/');
    }
}