import { DOCUMENT, inject, Injectable, signal } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class ThemeService {
    private readonly document = inject(DOCUMENT);
    private readonly currentTheme = signal<Theme>('dark');

    constructor() {
        this.setTheme(this.getThemeFromLocalStorage());
    }

    public toggleTheme() {
        if (this.currentTheme() === 'light')
            this.setTheme('dark');
        else
            this.setTheme('light');
    }

    private setTheme(theme: Theme) {
        this.currentTheme.set(theme);
        this.document.documentElement.setAttribute('data-theme', theme);
        this.setThemeInLocalStorage(theme);
    }

    private setThemeInLocalStorage(theme: Theme) {
        localStorage.setItem('preferredTheme', theme);
    }

    private getThemeFromLocalStorage(): Theme {
        return (localStorage.getItem('preferredTheme') as Theme) ?? 'dark';
    }
}

export type Theme = 'light' | "dark";