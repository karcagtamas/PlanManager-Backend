import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IRootState } from '../models';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  private tokenExists: boolean = !!localStorage.getItem('token');

  // tslint:disable-next-line: variable-name
  private readonly _state = new BehaviorSubject<IRootState>({
    token: this.tokenExists ? localStorage.getItem('token') : '',
    userId: null,
    isLoggedIn: this.tokenExists
  });

  private state$ = this._state.asObservable();

  constructor() {}

  public get isLoggedIn(): boolean {
    return this._state.getValue().isLoggedIn;
  }

  public set isLoggedIn(value: boolean) {
    const state: IRootState = this._state.getValue();
    state.isLoggedIn = value;
    this._state.next(state);
  }

  public get userId(): string {
    return this._state.getValue().userId;
  }

  public set userId(value: string) {
    const state: IRootState = this._state.getValue();
    state.userId = value;
    this._state.next(state);
  }

  public get token(): string {
    return this._state.getValue().token;
  }

  public set token(value: string) {
    const state: IRootState = this._state.getValue();
    state.token = value;
    this._state.next(state);
  }
}
