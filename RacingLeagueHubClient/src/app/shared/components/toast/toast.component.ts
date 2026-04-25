
import { Component, inject } from "@angular/core";
import { ToastService } from "../../../core/services/toast.service";

@Component({
    selector: 'toast-container',
    template: `
    <!-- border-info border-success border-warning border-error -->
    <!-- text-info text-success text-warning text-error -->
    <div class="toast-wrapper">
        @for (toast of toastService.toasts(); track toast.id) {
            <div class="toast-card bg-base-200 border-l-4" 
                [class]="'border-' + toast.type"
                (click)="toastService.remove(toast.id)"
                animate.enter="fade-slide-in"
                animate.leave="fade-slide-out">
                <h4 [class]="'text-' + toast.type" class="capitalize font-bold">{{ toast.type }}</h4>
                <span> {{ toast.message }} </span>
            </div>
        }
    </div>
  `,
    styles: [`
    .toast-wrapper {
        position: fixed;
        bottom: 1em;
        right: 1em;
        z-index: 9999;
        display: flex;
        flex-direction: column;
        gap: 0.5em;
    }
    .toast-card {
        padding: .5em 1em;
        border-radius: 0.25em;
        cursor: pointer;
    }
    
    @keyframes fadeSlideIn {
        from {
            opacity: 0;
            translate: 1em 0;
        }
    }

    .fade-slide-in {
        animation: fadeSlideIn 300ms;
    }

    .fade-slide-out {
        animation: fadeSlideIn 100ms ease-in reverse;
    }
    `]
})
export class ToastContainerComponent {
    protected readonly toastService = inject(ToastService);
}