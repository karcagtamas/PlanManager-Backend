import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { OwnMovieDTO, MovieDTO, MovieListDTO } from 'src/app/models';
import { MovieService, UtilService } from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { IModalResponse } from 'src/app/interfaces';
import { HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatDialogRef } from '@angular/material/dialog';

/**
 * Movies Mapper Modal Componenet
 * Modal for mapping movies to current user
 * @export
 * @class MoviesMapperModalComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-movies-mapper-modal',
  templateUrl: './movies-mapper-modal.component.html',
  styleUrls: ['./movies-mapper-modal.component.scss']
})
export class MoviesMapperModalComponent implements OnInit {
  displayedColumns: string[] = ['title', 'year'];
  allMoviesSource: MovieListDTO[] = [];
  selectedMoviesSource: MovieListDTO[] = [];
  allMovies: MatTableDataSource<MovieListDTO>;
  selectedMovies: MatTableDataSource<MovieListDTO>;
  selectedSelectedMovie: MovieListDTO = null;
  selectedAllMovie: MovieListDTO = null;

  @ViewChild(MatSort, { static: true }) sort: MatSort;

  /**
   * Creates an instance of MoviesMapperModalComponent.
   * @param {MatDialogRef<MoviesMapperModalComponent>} dialogRef
   * @param {MovieService} movieService
   * @param {ToastrService} toastr
   * @param {UtilService} utilService
   * @memberof MoviesMapperModalComponent
   */
  constructor(
    public dialogRef: MatDialogRef<MoviesMapperModalComponent>,
    private movieService: MovieService,
    private toastr: ToastrService,
    private utilService: UtilService
  ) {}

  /**
   * On Init hook
   * Gets movie lists (all, selected)
   * @memberof MoviesMapperModalComponent
   */
  ngOnInit() {
    this.allMovies = new MatTableDataSource<MovieListDTO>();
    this.selectedMovies = new MatTableDataSource<MovieListDTO>();
    this.getMovies();
  }

  /**
   * Gets all movies after gets own movies
   * @memberof MoviesMapperModalComponent
   */
  public getMovies(): void {
    this.movieService
      .getMovies()
      .then(res => {
        this.allMoviesSource = res;

        this.movieService
          .getOwnMovies()
          .then(res2 => {
            this.selectedMoviesSource = res2.map(x => {
              const listDto: MovieListDTO = {
                id: x.id,
                title: x.title,
                description: x.description,
                year: x.year,
                creater: ''
              };
              return listDto;
            });
            this.setTables();
          })
          .catch(() => {
            this.selectedMoviesSource = [];
          });
      })
      .catch(() => {
        this.allMoviesSource = [];
      });
  }

  /**
   * Sets up data tables
   * Filters all movie list with the selected list
   * Sets up sorting
   * @memberof MoviesMapperModalComponent
   */
  public setTables(): void {
    const all: MovieListDTO[] = this.allMoviesSource.filter(x => {
      return this.selectedMoviesSource.filter(y => y.id === x.id).length === 0;
    });
    this.allMovies = new MatTableDataSource<MovieListDTO>(all);
    this.allMovies.sort = this.sort;

    this.selectedMovies = new MatTableDataSource<MovieListDTO>(
      this.selectedMoviesSource
    );
    this.selectedMovies.sort = this.sort;
  }

  /**
   * Exits from modal without any action
   * @memberof MoviesMapperModalComponent
   */
  public exitWithoutAction(): void {
    this.dialogRef.close();
  }

  /**
   * Selects a movie from the selected movie list
   * @param {MovieListDTO} movie Movie element
   * @memberof MoviesMapperModalComponent
   */
  public setSelectedMovieAsSelected(movie: MovieListDTO): void {
    this.selectedSelectedMovie = movie;
  }

  /**
   * Selects a movie from the all movie list
   * @param {MovieListDTO} movie Movie element
   * @memberof MoviesMapperModalComponent
   */
  public setAllMovieAsSelected(movie: MovieListDTO): void {
    this.selectedAllMovie = movie;
  }

  /**
   * Transfers movie from the all movie list to the selected movie list
   * @memberof MoviesMapperModalComponent
   */
  public transferMovieToSelectedList(): void {
    if (this.selectedAllMovie !== null) {
      this.selectedMoviesSource.unshift(this.selectedAllMovie);
      this.selectedAllMovie = null;
      this.setTables();
    }
  }

  /**
   * Transfers movie from the selected movie list to the all movie list
   * @memberof MoviesMapperModalComponent
   */
  public transferMovieToAllList(): void {
    if (this.selectedSelectedMovie !== null) {
      this.selectedMoviesSource = this.selectedMoviesSource.filter(
        x => x.id !== this.selectedSelectedMovie.id
      );
      this.selectedSelectedMovie = null;
      this.setTables();
    }
  }

  /**
   * Saves mapping changes after closes the modal
   * @memberof MoviesMapperModalComponent
   */
  public save(): void {
    if (this.selectedMoviesSource.length) {
      const response: IModalResponse = {
        data: null,
        isSuccess: true,
        needRefresh: true,
        isEdit: true
      };
      this.movieService
        .updateMovieMappings(this.selectedMoviesSource)
        .then(() => {
          this.toastr.success('', this.utilService.updateSuccess);
          this.dialogRef.close(response);
        })
        .catch((err: HttpErrorResponse) => {
          this.toastr.error(err.message, this.utilService.updateFailed);
        });
    }
  }
}
