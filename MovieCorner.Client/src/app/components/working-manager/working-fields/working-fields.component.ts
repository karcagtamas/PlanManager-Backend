import { Component, OnInit, Input, ViewChild, OnChanges } from '@angular/core';
import { WorkingDayListDTO, WorkingFieldDTO } from 'src/app/models';
import { WorkingManagerService } from 'src/app/services/working-manager.service';
import { Router } from '@angular/router';
import { UtilService } from 'src/app/services';
import { IModalResponse, IWorkingFieldModalData } from 'src/app/interfaces';
import { WorkingDayModalComponent } from '../working-day-modal/working-day-modal.component';
import { IWorkingDayModalData } from 'src/app/interfaces/IWorkingDayModalData';
import { WorkingFieldModalComponent } from '../working-field-modal/working-field-modal.component';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'ks-working-fields',
  templateUrl: './working-fields.component.html',
  styleUrls: ['./working-fields.component.scss']
})
export class WorkingFieldsComponent implements OnInit, OnChanges {
  @Input() dateString: string;
  day: Date;
  workingDay: WorkingDayListDTO;
  displayedColumns: string[] = ['title', 'description', 'length'];
  fieldList: MatTableDataSource<WorkingFieldDTO>;
  totalLength = 0;
  determinedLength = 0;

  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(
    private workingManagerService: WorkingManagerService,
    private dialog: MatDialog,
    private router: Router,
    private utilService: UtilService
  ) {}

  ngOnInit() {
    this.day = new Date(this.dateString);
    this.getWorkingDay();
  }

  ngOnChanges() {
    this.day = new Date(this.dateString);
    this.getWorkingDay();
  }

  public getWorkingDay(): void {
    this.workingManagerService
      .getWorkingDay(this.day)
      .then(res => {
        this.workingDay = res;

        if (this.workingDay !== null) {
          this.fieldList = new MatTableDataSource<WorkingFieldDTO>(
            this.workingDay.workingFields
          );
          this.fieldList.sort = this.sort;
        } else {
          this.fieldList = new MatTableDataSource<WorkingFieldDTO>();
          this.fieldList.sort = this.sort;
        }
        this.setTypeColor();
        this.calculateDeterminedLength();
        this.calculateTotalLength();
      })
      .catch(() => {
        this.workingDay = null;
        this.fieldList = new MatTableDataSource<WorkingFieldDTO>();
        this.fieldList.sort = this.sort;
      });
  }

  public calculateTotalLength(): void {
    if (this.workingDay === null) {
      this.totalLength = 0;
      return;
    }
    let length = 0;
    for (const i of this.workingDay.workingFields) {
      length += i.length;
    }
    this.totalLength = length;
  }

  public calculateDeterminedLength(): void {
    if (this.workingDay === null) {
      this.determinedLength = 0;
      return;
    }
    let length = 0;
    const startInMin =
      this.workingDay.startHour * 60 + this.workingDay.startMin;
    const endInMin = this.workingDay.endHour * 60 + this.workingDay.endMin;
    length = (endInMin - startInMin) / 60;
    this.determinedLength = length;
  }

  public redirectToTheNextDay(): void {
    const newDate: Date = this.day;
    newDate.setDate(this.day.getDate() + 1);
    this.router.navigateByUrl(
      `/working-manager/${this.utilService.toDateString(newDate)}`
    );
  }

  public redirectToThePrevDay(): void {
    const newDate: Date = this.day;
    newDate.setDate(this.day.getDate() - 1);
    this.router.navigateByUrl(
      `/working-manager/${this.utilService.toDateString(newDate)}`
    );
  }

  public openSettingsModal(): void {
    const data: IWorkingDayModalData = {
      day: this.day,
      workingDay: this.workingDay
    };
    const dialogRef = this.dialog.open(WorkingDayModalComponent, {
      minWidth: '80%',
      minHeight: '80%',
      data
    });
    dialogRef.afterClosed().subscribe((res: IModalResponse | undefined) => {
      if (res !== undefined && res.needRefresh) {
        this.getWorkingDay();
      }
    });
  }

  public openFieldModal(field: WorkingFieldDTO): void {
    const data: IWorkingFieldModalData = {
      workingDay: this.workingDay,
      workingField: field
    };
    const dialogRef = this.dialog.open(WorkingFieldModalComponent, {
      minWidth: '80%',
      minHeight: '80%',
      data
    });
    dialogRef.afterClosed().subscribe((res: IModalResponse | undefined) => {
      if (res !== undefined && res.needRefresh) {
        this.getWorkingDay();
      }
    });
  }

  public setTypeColor(): void {
    const div: HTMLElement = document.getElementById('frame');
    if (div !== null) {
      div.classList.remove('workDay');
      div.classList.remove('weekend');
      div.classList.remove('free');
      div.classList.remove('university');
      div.classList.remove('withoutWorkDay');
      if (this.workingDay !== null) {
        switch (this.workingDay.type.id) {
          case 1:
            div.classList.add('workDay');
            break;
          case 2:
            div.classList.add('weekend');
            break;
          case 3:
            div.classList.add('free');
            break;
          case 4:
            div.classList.add('university');
            break;
          case 5:
            div.classList.add('withoutWorkDay');
            break;
        }
      }
    }
  }
}
