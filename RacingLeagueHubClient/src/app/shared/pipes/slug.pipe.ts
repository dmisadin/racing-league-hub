import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ 
    name: 'slug', 
    standalone: true 
})
export class SlugPipe implements PipeTransform {
    transform(value?: string): string {
        if (!value) return "";
        return value
            .toLowerCase()
            .normalize('NFD')                    // decompose accented chars: é → e + ́
            .replace(/[\u0300-\u036f]/g, '')     // strip diacritic marks
            .replace(/[^a-z0-9\s-]/g, '')        // remove anything not alphanumeric, space, or hyphen
            .trim()
            .replace(/[\s_]+/g, '-')             // spaces and underscores to hyphens
            .replace(/-{2,}/g, '-');             // collapse multiple hyphens
    }
}