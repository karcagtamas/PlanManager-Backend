import { Component, OnInit, ViewChild } from '@angular/core';
import { MovieService } from 'src/app/services/movie.service';
import { MovieListDTO, MovieDTO } from 'src/app/models';
import { MatDialog } from '@angular/material/dialog';
import { MoviesModalComponent } from '../movies-modal/movies-modal.component';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { IModalResponse, IConfirmData } from 'src/app/interfaces';
import {
  trigger,
  state,
  transition,
  animate,
  style
} from '@angular/animations';
import { UtilService, NotificationService } from 'src/app/services';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

/**
 * Movies All Component
 * Display all existing movies what are manageable
 * @export
 * @class MoviesAllComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-movies-all',
  templateUrl: './movies-all.component.html',
  styleUrls: ['./movies-all.component.scss'],
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
export class MoviesAllComponent implements OnInit {
  dataSource: MatTableDataSource<MovieListDTO>;
  displayedColumns: string[] = ['title', 'year', 'creater'];
  filterValue = '';

  expanded: MovieListDTO | null;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  /**
   * Creates an instance of MoviesAllComponent.
   * @param {MovieService} movieService
   * @param {MatDialog} dialog
   * @param {ToastrService} toastr
   * @param {UtilService} utilsService
   * @param {NotificationService} notificationService
   * @memberof MoviesAllComponent
   */
  constructor(
    private movieService: MovieService,
    private dialog: MatDialog,
    private toastr: ToastrService,
    private utilsService: UtilService,
    private notificationService: NotificationService
  ) {}

  /**
   * On Init hook
   * Gets movies
   * @memberof MoviesAllComponent
   */
  ngOnInit() {
    this.dataSource = new MatTableDataSource();
    this.getMovies();
  }

  /**
   * Gets all movies and set sorter and paginator
   * @memberof MoviesAllComponent
   */
  public getMovies(): void {
    this.movieService
      .getMovies()
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
   * Opens movie creater modal
   * @memberof MoviesAllComponent
   */
  public create(): void {
    this.openDialog(null);
  }

  /**
   * Gets movie element and opens update modal
   * @param {number} id
   * @memberof MoviesAllComponent
   */
  public update(id: number): void {
    this.movieService
      .getMovie(id)
      .then(res => {
        this.openDialog(res);
      })
      .catch((err: HttpErrorResponse) => {
        this.toastr.error(err.message);
      });
  }

  /**
   * Opens a dialog for managing movie
   * Update or creater
   * @param {MovieDTO} data Movie data element
   * @memberof MoviesAllComponent
   */
  public openDialog(data: MovieDTO): void {
    const dialogRef = this.dialog.open(MoviesModalComponent, {
      data,
      minWidth: '80%',
      minHeight: '80%'
    });

    dialogRef.afterClosed().subscribe((res: IModalResponse | undefined) => {
      if (res !== null && res !== undefined && res.needRefresh) {
        this.getMovies();
      }
    });
  }

  /**
   * Filters the table elements by the filtering value
   * @memberof MoviesAllComponent
   */
  public filter(): void {
    this.dataSource.filter = this.filterValue.trim().toLowerCase();

    // Set paginator back to the first page
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  /**
   * Delete movie by the movie element
   * @param {MovieListDTO} movie Movie element
   * @memberof MoviesAllComponent
   */
  public delete(movie: MovieListDTO): void {
    const data: IConfirmData = { title: 'Movie', name: movie.title };
    this.notificationService.confirm(data).then(res => {
      if (res) {
        this.movieService
          .deleteMovie(movie.id)
          .then(() => {
            this.toastr.success('', this.utilsService.deleteSuccess);
            this.getMovies();
          })
          .catch((err: HttpErrorResponse) => {
            this.toastr.error(err.message, this.utilsService.deleteFailed);
          });
      }
    });
  }
}
