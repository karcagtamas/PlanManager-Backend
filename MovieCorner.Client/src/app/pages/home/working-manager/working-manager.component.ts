import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UtilService } from 'src/app/services';

/**
 * Working Manager Componenet - Working Manager pages
 * @export
 * @class WorkingManagerComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-working-manager',
  templateUrl: './working-manager.component.html',
  styleUrls: ['./working-manager.component.scss']
})
export class WorkingManagerComponent implements OnInit {
  dateString = '';
  day: Date = new Date();
  dayName = '';
  dayNames = [
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday'
  ];

  /**
   * Creates an instance of WorkingManagerComponent.
   * @memberof WorkingManagerComponent
   */
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private utilService: UtilService
  ) {}

  /**
   * On Init hook
   * @memberof WorkingManagerComponent
   */
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.dateString = this.route.snapshot.params.date;
      if (this.dateString === undefined) {
        const s = this.utilService.toDateString(new Date());
        this.router.navigateByUrl(`/working-manager/${s}`);
      }
      this.day = new Date(this.dateString);
      this.dayName = this.dayNames[this.day.getDay()];
    });
  }
}
