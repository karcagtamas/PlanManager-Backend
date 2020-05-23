import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService, UtilService } from 'src/app/services';
import { UserProfileDTO, UserProfileUpdateDTO } from 'src/app/models';
import { ToastrService } from 'ngx-toastr';

/**
 * User Modify Componenet
 * Page for managing user settings and datas
 * @export
 * @class UserModifyComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-user-modify',
  templateUrl: './user-modify.component.html',
  styleUrls: ['./user-modify.component.scss']
})
export class UserModifyComponent implements OnInit {
  userProfile: UserProfileDTO = null;
  form: FormGroup;

  /**
   * Creates an instance of UserModifyComponent.
   * @param {FormBuilder} formBuilder
   * @param {Router} router
   * @param {UserService} userService
   * @param {ToastrService} toastrService
   * @param {UtilService} utilService
   * @memberof UserModifyComponent
   */
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService,
    private toastrService: ToastrService,
    public utilService: UtilService
  ) {}

  /**
   * On Init hook
   * Get user profile
   * Sets up the form
   * @memberof UserModifyComponent
   */
  ngOnInit() {
    this.getUserProfile();
    this.form = this.formBuilder.group({
      fullName: ['', [Validators.maxLength(150)]],
      allergy: [''],
      class: ['', [Validators.maxLength(5)]],
      tShirtSize: ['', [Validators.maxLength(5)]]
    });
  }

  /**
   * Saves modifications
   * @memberof UserModifyComponent
   */
  public save(): void {
    // Check form
    if (!this.form.invalid) {
      const result: UserProfileUpdateDTO = this.form.value;
      result.id = this.userProfile.id;
      // Send update request
      this.userService
        .updateUser(result)
        .then(() => {
          this.toastrService.success('', this.utilService.updateSuccess);
        })
        .catch(() => {
          this.toastrService.error('', this.utilService.updateFailed);
        });
    } else {
      this.toastrService.warning(
        this.utilService.formIsInvalidMessage(),
        this.utilService.updateFailed
      );
    }
  }

  /**
   * Cancel modification and redirect to the deatil page
   * @memberof UserModifyComponent
   */
  public cancel(): void {
    this.router.navigateByUrl('/profile');
  }

  /**
   * Gets user profile
   * @memberof UserModifyComponent
   */
  public getUserProfile(): void {
    this.userService
      .getUser()
      .then(res => {
        this.userProfile = res;
        this.setForm();
      })
      .catch(() => {
        this.userProfile = null;
      });
  }

  /**
   * Sets form datas
   * @memberof UserModifyComponent
   */
  public setForm(): void {
    const values: UserProfileUpdateDTO = {
      fullName: this.isValid(this.userProfile.fullName),
      allergy: this.isValid(this.userProfile.allergy),
      class: this.isValid(this.userProfile.class),
      tShirtSize: this.isValid(this.userProfile.tShirtSize)
    };
    this.form.setValue(values);
  }

  /**
   * Checks the input value is valid or not
   * If is it valid, it will return with the original value else ith will give back empty string
   * @param {*} value Value to check
   * @returns {*} Result value
   * @memberof UserModifyComponent
   */
  public isValid(value: any): any {
    if (value === null) {
      return '';
    }
    return value;
  }
}
