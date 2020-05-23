import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginTitle, RegistrationTitle } from 'src/app/services/page.service';

/**
 * User Component - Main user page for login and registration
 * @export
 * @class UserComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  title = '';
  loginTitle = LoginTitle;
  registrationTitle = RegistrationTitle;

  /**
   * Creates an instance of UserComponent.
   * @param {ActivatedRoute} route
   * @memberof UserComponent
   */
  constructor(private route: ActivatedRoute) {
    // Subscribe on childs datas for the title
    route.url.subscribe(() => {
      this.title = this.route.snapshot.firstChild.data.title;
    });
  }

  /**
   * On Init hook
   * @memberof UserComponent
   */
  ngOnInit() {}
}
