import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'isMimeTypeImage',
    standalone: true,
    pure: true
})
export class IsMimeTypeImagePipe implements PipeTransform {
    transform(mimeType?: string): boolean {
        if (!mimeType) return false;
        return mimeType.startsWith('image/');
    }
}