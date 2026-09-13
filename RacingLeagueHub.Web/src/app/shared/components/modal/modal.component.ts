import { Component, afterNextRender, effect, input, model, output, viewChild, ElementRef } from '@angular/core';

@Component({
    selector: 'modal',
    template: `
    <dialog #dlg class="modal" (close)="handleClose()" (cancel)="handleCancel($event)">
        <div class="modal-box overflow-y-visible">
            <h1 class="text-lg font-bold mb-4 w-full">{{ title() }}</h1>
            <ng-content></ng-content>
        </div>

        <!-- backdrop click closes the dialog (native behavior for method="dialog") -->
        <form method="dialog" class="modal-backdrop">
            <button aria-label="Close">close</button>
        </form>
    </dialog>
  `,
})
export class ModalComponent {
    open = model<boolean>(false);
    openByDefault = input<boolean>(false);
    disableEsc = input<boolean>(false);
    disableBackdropClose = input<boolean>(false);
    title = input<string>("Modal title");

    readonly onOpen = output<void>();
    readonly onClose = output<void>();
    readonly onDiscard = output<void>();

    private dlgRef = viewChild.required<ElementRef<HTMLDialogElement>>('dlg');

    constructor() {
        // Open on mount if requested
        afterNextRender(() => {
            if (this.openByDefault()) this.open.set(true);
        });

        // Sync `open()` -> actual <dialog> state
        effect(() => {
            const dlg = this.dlgRef().nativeElement;
            const shouldBeOpen = this.open();

            if (shouldBeOpen && !dlg.open) {
                dlg.showModal();
                this.onOpen.emit();
            }

            if (!shouldBeOpen && dlg.open) {
                dlg.close();
                // close event will fire and call handleClose()
            }
        });
    }

    // Public imperative helpers (optional)
    show() {
        this.open.set(true);
    }

    close() {
        this.open.set(false);
    }

    discard() {
        // "Discard" is an app-level concept; we emit and then close
        this.onDiscard.emit();
        this.open.set(false);
    }

    // ---- Native dialog events ----

    handleClose() {
        // If something closed the dialog (ESC/backdrop/button),
        // reflect it back into our model, then notify.
        if (this.open()) this.open.set(false);

        // If backdrop-close is disabled, re-open immediately (unless app closed it).
        // We can detect by checking disableBackdropClose and whether open() was set false by the app.
        // Here, since we just set open(false), we only reopen when disableBackdropClose is true
        // AND the close was not triggered via code path that calls close().
        if (this.disableBackdropClose()) {
            // Reopen on next tick to avoid "already open" errors
            queueMicrotask(() => this.open.set(true));
            return;
        }

        this.onClose.emit();
    }

    handleCancel(ev: Event) {
        // cancel = ESC key (and some UA behaviors)
        if (this.disableEsc()) {
            ev.preventDefault();
            return;
        }
        // if not prevented, dialog will close and handleClose() will run
    }
}
