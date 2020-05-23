import { ISideMenuItem } from './ISideMenuItem';

/**
 * Root state's interface
 * @export
 * @interface IRootState
 */
export interface IRootState {
  isLoggedIn: boolean;
  userId: number;
  token: string;
  oactsTitle: string;
  sideMenuItems: ISideMenuItem[];
  sideNavIsOpen: boolean;
}
