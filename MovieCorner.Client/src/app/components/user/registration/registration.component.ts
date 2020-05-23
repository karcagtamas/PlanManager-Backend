import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService, CommonService, UtilService } from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { IRegistrationDatas } from 'src/app/interfaces/IRegistrationDatas';

/**
 * Registration Component - Registration page
 * @export
 * @class RegistrationComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  form: FormGroup;

  /**
   * Creates an instance of RegistrationComponent.
   * @param {FormBuilder} formBuilder
   * @param {UserService} userService
   * @param {ToastrService} toastr
   * @param {CommonService} commonService
   * @param {UtilService} utilService
   * @memberof RegistrationComponent
   */
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private toastr: ToastrService,
    private commonService: CommonService,
    public utilService: UtilService
  ) {}

  /**
   * On Init hook
   * Set up form
   * @memberof RegistrationComponent
   */
  ngOnInit() {
    this.form = this.formBuilder.group({
      userName: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(200)
        ]
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(200)
        ]
      ],
      passwordConfirm: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      fullName: ['', [Validators.maxLength(150)]]
    });
  }

  /**
   * Read in registration form, and send registration request
   * @memberof RegistrationComponent
   */
  public registration(): void {
    // Check form
    if (this.form.invalid) {
      this.toastr.warning(
        this.utilService.formIsInvalidMessage(),
        this.utilService.registrationFailed
      );
    } else {
      const reg: IRegistrationDatas = this.form.value;
      // Check password and confirm password are same
      if (reg.password === reg.passwordConfirm) {
        // Send registration request
        this.userService
          .registration(reg)
          .then(res => {
            // Registration request was success
            if (res.succeeded) {
              this.toastr.success(
                'The registration was successfully',
                this.utilService.registrationSuccess
              );
            } else {
              if (res.errors && res.errors[0]) {
                this.toastr.error(
                  res.errors[0],
                  this.utilService.registrationFailed
                );
              }
            }
          })
          .catch(err => {
            this.toastr.error(
              err.error.message,
              this.utilService.registrationFailed
            );
          });
      } else {
        this.toastr.warning(
          this.utilService.incorrectConfirmPasswordMessage,
          this.utilService.registrationFailed
        );
      }
    }
  }
}
