import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IRootState } from '../interfaces';
import { ISideMenuItem } from '../interfaces/ISideMenuItem';

/**
 * Common Service for managing a state
 *
 * @export
 * @class CommonService
 */
@Injectable({
  providedIn: 'root'
})
export class CommonService {
  tokenExists: boolean = !!localStorage.getItem('token');

  /**
   * Creates an instance of CommonService.
   * @memberof CommonService
   */
  constructor() {}

  /**
   * State
   * @private
   * @memberof CommonService
   */
  // tslint:disable-next-line: variable-name
  private readonly _state = new BehaviorSubject<IRootState>({
    isLoggedIn: this.tokenExists,
    userId: null,
    token: this.tokenExists ? localStorage.getItem('token') : '',
    oactsTitle: '',
    sideMenuItems: [],
    sideNavIsOpen: false
  });

  /**
   * Observable state
   * @memberof CommonService
   */
  readonly state$ = this._state.asObservable();

  /**
   * Gets logged in status
   * @type {boolean} Is logged in or not
   * @memberof CommonService
   */
  public get isLoggedIn(): boolean {
    return this._state.getValue().isLoggedIn;
  }

  /**
   * Sets logged in status
   * @memberof CommonService
   */
  public set isLoggedIn(val: boolean) {
    const state: IRootState = this._state.getValue();
    state.isLoggedIn = val;
    this._state.next(state);
  }

  /**
   * Gets user's Id
   * @type {number} Number
   * @memberof CommonService
   */
  public get userId(): number {
    return this._state.getValue().userId;
  }

  /**
   * Sets user's Id
   *
   * @memberof CommonService
   */
  public set userId(val: number) {
    const state: IRootState = this._state.getValue();
    state.userId = val;
    this._state.next(state);
  }

  /**
   * Gets token
   * @type {string} Token
   * @memberof CommonService
   */
  public get token(): string {
    return this._state.getValue().token;
  }

  /**
   * Sets token
   * @memberof CommonService
   */
  public set token(val: string) {
    const state: IRootState = this._state.getValue();
    state.token = val;
    this._state.next(state);
  }

  /**
   * Gets oacts title
   * @type {string} Title
   * @memberof CommonService
   */
  public get oactsTitle(): string {
    return this._state.getValue().oactsTitle;
  }

  /**
   * Sets oact title
   * @memberof CommonService
   */
  public set oactsTitle(val: string) {
    const state: IRootState = this._state.getValue();
    state.oactsTitle = val;
    this._state.next(state);
  }

  /**
   * Gets side menu items
   * @type {ISideMenuItem[]} Side menu items
   * @memberof CommonService
   */
  public get sideMenuItems(): ISideMenuItem[] {
    return this._state.getValue().sideMenuItems;
  }

  /**
   * Sets side menu items
   * @memberof CommonService
   */
  public set sideMenuItems(val: ISideMenuItem[]) {
    const state: IRootState = this._state.getValue();
    state.sideMenuItems = val;
    this._state.next(state);
  }

  /**
   * Gets side nav open status
   * @type {boolean} Is open or not
   * @memberof CommonService
   */
  public get sideNavIsOpen(): boolean {
    return this._state.getValue().sideNavIsOpen;
  }

  /**
   * Sets side nav open status
   * @memberof CommonService
   */
  public set sideNavIsOpen(val: boolean) {
    const state: IRootState = this._state.getValue();
    state.sideNavIsOpen = val;
    this._state.next(state);
  }
}
