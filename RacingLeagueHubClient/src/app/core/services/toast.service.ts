import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToastService {
    readonly toasts = signal<Toast[]>([]);

    public showSuccess(message: string, duration = 7000) {
        this.show(message, "success", duration);
    }

    public showError(message: string, duration = 7000) {
        this.show(message, "error", duration);
    }

    public showWarning(message: string, duration = 7000) {
        this.show(message, "warning", duration);
    }

    public show(message: string, type: Toast['type'] = 'info', duration = 7000) {
        const id = Date.now();
        const newToast = { id, message, type };

        this.toasts.update(current => [...current, newToast]);

        setTimeout(() => {
            this.remove(id);
        }, duration);
    }

    public remove(id: number) {
        this.toasts.update(current =>
            current.filter(t => t.id !== id)
        );
    }
}

export interface Toast {
    id: number;
    message: string;
    type: 'success' | 'error' | 'info' | 'warning';
}
