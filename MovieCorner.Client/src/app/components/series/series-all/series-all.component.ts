import { Component, OnInit, ViewChild } from '@angular/core';
import {
  animate,
  state,
  style,
  transition,
  trigger
} from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { MovieListDTO, SeriesListDTO, SeriesDTO } from '../../../models';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import {
  MovieService,
  NotificationService,
  SeriesService,
  UtilService
} from '../../../services';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { SeriesModalComponent } from '../series-modal/series-modal.component';
import { IModalResponse } from 'src/app/interfaces';

/**
 * Series All Component
 * Displays all existing series
 * @export
 * @class SeriesAllComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-series-all',
  templateUrl: './series-all.component.html',
  styleUrls: ['./series-all.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      )
    ])
  ]
})
export class SeriesAllComponent implements OnInit {
  dataSource: MatTableDataSource<SeriesListDTO>;
  displayedColumns: string[] = [
    'title',
    'startYear',
    'endYear',
    'creater',
    'creationTime',
    'countOfSeasons'
  ];
  filterValue = '';

  expanded: SeriesListDTO | null;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  /**
   * Creates an instance of SeriesAllComponent.
   * @memberof SeriesAllComponent
   */
  constructor(
    private seriesService: SeriesService,
    private dialog: MatDialog,
    private toastr: ToastrService,
    private utilsService: UtilService,
    private notificationService: NotificationService
  ) {}

  /**
   * On Init hook
   * @memberof SeriesAllComponent
   */
  ngOnInit() {
    this.dataSource = new MatTableDataSource<SeriesListDTO>();
    this.getSeries();
  }

  public getSeries(): void {
    this.seriesService
      .getAllSeries()
      .then(res => {
        this.dataSource = new MatTableDataSource<SeriesListDTO>(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      })
      .catch(() => {
        this.dataSource = new MatTableDataSource<SeriesListDTO>();
      });
  }

  /**
   * Filters the table elements by the filtering value
   * @memberof SeriesAllComponent
   */
  public filter(): void {
    this.dataSource.filter = this.filterValue.trim().toLowerCase();

    // Set paginator back to the first page
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  /**
   * Opens a dialog for managing movie
   * Update or creater
   * @param {SeriesDTO} data Movie data element
   * @memberof SeriesAllComponent
   */
  public openDialog(data: SeriesDTO): void {
    const dialogRef = this.dialog.open(SeriesModalComponent, {
      data,
      minWidth: '80%',
      minHeight: '80%'
    });

    dialogRef.afterClosed().subscribe((res: IModalResponse | undefined) => {
      if (res !== null && res !== undefined && res.needRefresh) {
        this.getSeries();
      }
    });
  }

  /**
   * Opens series creater modal
   * @memberof SeriesAllComponent
   */
  public create(): void {
    this.openDialog(null);
  }
}
