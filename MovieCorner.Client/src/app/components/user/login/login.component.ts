import { Component, OnInit, AfterContentChecked } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService, CommonService, UtilService } from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { ILoginDatas } from 'src/app/interfaces';
import { Router, ActivatedRoute } from '@angular/router';

/**
 * Login Componenet - Login page
 * @export
 * @class LoginComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  /**
   * Creates an instance of LoginComponent.
   * @param {FormBuilder} formBuilder
   * @param {UserService} userService
   * @param {ToastrService} toastr
   * @param {CommonService} commonService
   * @param {UtilService} utilService
   * @param {Router} router
   * @memberof LoginComponent
   */
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private toastr: ToastrService,
    private commonService: CommonService,
    public utilService: UtilService,
    private router: Router
  ) {}

  /**
   * On Init hook
   * Set up form
   * @memberof LoginComponent
   */
  ngOnInit() {
    this.form = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  /**
   * Read in login form, and send login request
   * Save token if request was successfully
   * @memberof LoginComponent
   */
  public login(): void {
    // Check form
    if (this.form.invalid) {
      this.toastr.warning(
        this.utilService.formIsInvalidMessage(),
        this.utilService.loginFailed
      );
    } else {
      const log: ILoginDatas = this.form.value;
      this.userService
        .login(log)
        .then(res => {
          if (res.token) {
            // Request was success
            // Save token
            this.commonService.token = res.token;
            this.commonService.isLoggedIn = true;
            localStorage.setItem('token', this.commonService.token);
            this.router.navigateByUrl('/');
          } else {
            this.toastr.error(
              'The login was unsuccessfully',
              this.utilService.loginFailed
            );
          }
        })
        .catch(err => {
          this.toastr.error(err.error.message, this.utilService.loginFailed);
        });
    }
  }
}
