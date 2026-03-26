import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'isImageExtension',
    standalone: true,
    pure: true
})
export class IsImageExtensionPipe implements PipeTransform {
    transform(extension?: string): boolean {
        if (!extension) return false;
        const imageExtensions = ["png", "jpg", "jpeg", "webp", "svg"]
        return imageExtensions.find(e => e === extension) ? true : false;
    }
}