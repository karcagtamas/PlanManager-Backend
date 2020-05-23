import { Injectable } from '@angular/core';
import { ISideMenuItem } from '../interfaces/ISideMenuItem';

export const RegistrationTitle = 'Registration';
export const LoginTitle = 'Login';
export const UserDetailsTitle = 'Details';
export const UserModificationTitle = 'Modify';
export const UserMessagesTitle = 'Messages';
export const UserNotificationsTitle = 'Notifications';
export const ToDoSolvedTitle = 'Solved ToDos';
export const ToDoUnSolvedTitle = 'Unsolved ToDos';
export const MoviesAllTitle = 'All Movies';
export const MoviesMyTitle = 'My Movies';

/**
 * Page Service managing the side menu bar's menu items
 * @export
 * @class PageService
 */
@Injectable({
  providedIn: 'root'
})
export class PageService {
  /**
   * Creates an instance of PageService.
   * @memberof PageService
   */
  constructor() {}

  /**
   * Gets Profile menu's pages
   * @returns {Promise<ISideMenuItem[]>} Array of the menu items
   * @memberof PageService
   */
  public getProfilePages(): Promise<ISideMenuItem[]> {
    return new Promise(resolve => {
      let array: ISideMenuItem[];
      array = [
        { title: 'Details', route: '/profile/details', icon: 'person' },
        { title: 'Modify', route: '/profile/modify', icon: 'create' },
        {
          title: 'Notifications',
          route: '/profile/notifications',
          icon: 'notification_important'
        },
        { title: 'Messages', route: '/profile/messages', icon: 'message' }
      ];
      resolve(array);
    });
  }

  /**
   * Gets Movies menu's pages
   * @returns {Promise<ISideMenuItem[]>} Array of the menu items
   * @memberof PageService
   */
  public getMoviesPages(): Promise<ISideMenuItem[]> {
    return new Promise(resolve => {
      let array: ISideMenuItem[];
      array = [
        { title: 'All Movies', route: '/movies/all', icon: null },
        { title: 'My Movies', route: '/movies/my', icon: null }
      ];
      resolve(array);
    });
  }

  /**
   * Gets Series menu's pages
   * @returns {Promise<ISideMenuItem[]>} Array of the menu items
   * @memberof PageService
   */
  public getSeriesPages(): Promise<ISideMenuItem[]> {
    return new Promise(resolve => {
      let array: ISideMenuItem[];
      array = [
        { title: 'All Series', route: '/series/all', icon: null },
        { title: 'My Series', route: '/series/my', icon: null }
      ];
      resolve(array);
    });
  }

  /**
   * Gets ToDos menu's pages
   * @returns {Promise<ISideMenuItem[]>} Array of the menu items
   * @memberof PageService
   */
  public getTodoPages(): Promise<ISideMenuItem[]> {
    return new Promise(resolve => {
      let array: ISideMenuItem[];
      array = [
        { title: 'ToDo', route: '/todos/unsolved', icon: null },
        { title: 'Solved', route: '/todos/solved', icon: null }
      ];
      resolve(array);
    });
  }
}
