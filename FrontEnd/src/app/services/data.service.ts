import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private registerAs = new BehaviorSubject<string>("client");
  currentRegisterAs = this.registerAs.asObservable();

  constructor() { }

  changeRegisterAs(s: string) {
    this.registerAs.next(s);
  }
}
