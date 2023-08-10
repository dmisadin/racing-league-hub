import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ButtonClickService {
    buttonClicked = new Subject();

    getButtonClicked() {
        return this.buttonClicked.asObservable();
    }
}
