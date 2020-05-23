import { Component, OnInit, ViewChild } from '@angular/core';
import {
  trigger,
  state,
  transition,
  animate,
  style
} from '@angular/animations';
import { OwnMovieDTO } from 'src/app/models';
import { ToastrService } from 'ngx-toastr';
import { UtilService, MovieService } from 'src/app/services';
import { MoviesMapperModalComponent } from '../movies-mapper-modal/movies-mapper-modal.component';
import { IModalResponse } from 'src/app/interfaces';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

/**
 * Movies My Component
 * Displays my mapped movies
 * @export
 * @class MoviesMyComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-movies-my',
  templateUrl: './movies-my.component.html',
  styleUrls: ['./movies-my.component.scss'],
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
export class MoviesMyComponent implements OnInit {
  dataSource: MatTableDataSource<OwnMovieDTO>;
  displayedColumns: string[] = ['title', 'year', 'seen'];
  filterValue = '';

  expanded: OwnMovieDTO | null;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  /**
   * Creates an instance of MoviesMyComponent.
   * @param {MovieService} movieService
   * @param {MatDialog} dialog
   * @param {ToastrService} toastr
   * @param {UtilService} utilsService
   * @memberof MoviesMyComponent
   */
  constructor(
    private movieService: MovieService,
    private dialog: MatDialog,
    private toastr: ToastrService,
    private utilsService: UtilService
  ) {}

  /**
   * On Init hook
   * Gets own movies
   * @memberof MoviesMyComponent
   */
  ngOnInit() {
    this.dataSource = new MatTableDataSource();
    this.getOwnMovies();
  }

  /**
   * Gets own movies, paginator and sorter
   * @memberof MoviesMyComponent
   */
  public getOwnMovies(): void {
    this.movieService
      .getOwnMovies()
      .then(res => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      })
      .catch(() => {
        this.dataSource = new MatTableDataSource();
      });
  }

  /**
   * Filters data table by the filtervalue
   * Sets the paginator to the first page
   * @memberof MoviesMyComponent
   */
  public filter(): void {
    this.dataSource.filter = this.filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  /**
   * Update seen status for a movie element
   * @param {OwnMovieDTO} movie Update movie element
   * @memberof MoviesMyComponent
   */
  public statusChange(movie: OwnMovieDTO): void {
    // Send update request
    this.movieService
      .updateSeenStatus(movie.id, !movie.seen)
      .then(() => {
        this.toastr.success('', this.utilsService.updateSuccess);
        this.getOwnMovies();
      })
      .catch(() => {
        this.toastr.error('', this.utilsService.updateFailed);
      });
  }

  /**
   * Open mapper dialog, to add another movies to own list
   * @memberof MoviesMyComponent
   */
  public openMapper(): void {
    const dialogRef = this.dialog.open(MoviesMapperModalComponent, {
      minWidth: '80%',
      minHeight: '80%'
    });

    dialogRef.afterClosed().subscribe((res: IModalResponse | undefined) => {
      if (res !== null && res !== undefined && res.needRefresh) {
        this.getOwnMovies();
      }
    });
  }
}
