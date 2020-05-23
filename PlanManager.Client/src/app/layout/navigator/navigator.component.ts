import { Component, OnInit } from '@angular/core';
import { StateService } from 'src/app/services';
import { Router } from '@angular/router';
import { User } from 'src/app/models';

@Component({
  selector: 'pm-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.scss']
})
export class NavigatorComponent implements OnInit {
  user: User = null;

  constructor(private stateService: StateService, private router: Router) {}

  ngOnInit(): void {
    this.getUser();
  }

  /**
   * Logout process
   * Clear state and local storage
   * @memberof NavigatorComponent
   */
  public logout(): void {
    this.stateService.token = '';
    this.stateService.userId = null;
    this.stateService.isLoggedIn = false;
    localStorage.removeItem('token');
    this.router.navigateByUrl('/auth');
  }

  private getUser() {
    this.user = null;
  }
}
