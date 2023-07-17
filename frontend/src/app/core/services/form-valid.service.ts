import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class FormValidService {
  constructor() {}

  private dataFromChild = new BehaviorSubject<any>({});
  private dataFromParent = new BehaviorSubject<any>({});

  sendDataFromChild(data: any) {
    this.dataFromChild.next(data);
  }
  sendDataFromParent(data: any) {
    this.dataFromParent.next(data);
  }

  getDataFromChild() {
    return this.dataFromChild.asObservable();
  }  
  getDataFromParent() {
    return this.dataFromParent.asObservable();
  }
}
