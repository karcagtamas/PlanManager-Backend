import { Component, OnInit } from '@angular/core';
import { UserProfileDTO } from 'src/app/models';
import { UserService } from 'src/app/services';

/**
 * User Details Compoenent
 * Display details about the current user
 * @export
 * @class UserDetailsComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {
  userProfile: UserProfileDTO = null;

  /**
   * Creates an instance of UserDetailsComponent.
   * @param {UserService} userService
   * @memberof UserDetailsComponent
   */
  constructor(private userService: UserService) {}

  /**
   * On Init hook
   * Gets user profile
   * @memberof UserDetailsComponent
   */
  ngOnInit() {
    this.getUserProfile();
  }

  /**
   * Get user profile
   * @memberof UserDetailsComponent
   */
  public getUserProfile() {
    this.userService
      .getUser()
      .then(res => {
        this.userProfile = res;
      })
      .catch(err => {
        this.userProfile = null;
      });
  }
}
