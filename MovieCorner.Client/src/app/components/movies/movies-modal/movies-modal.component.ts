import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MovieDTO } from 'src/app/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UtilService } from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { IModalResponse } from 'src/app/interfaces';
import { MovieService } from 'src/app/services/movie.service';
import { HttpErrorResponse } from '@angular/common/http';

/**
 * Movies Modal Componenet
 * Modal for create or update movie elements
 * @export
 * @class MoviesModalComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-movies-modal',
  templateUrl: './movies-modal.component.html',
  styleUrls: ['./movies-modal.component.scss']
})
export class MoviesModalComponent implements OnInit {
  title = 'Create';
  isUpdate = false;
  form: FormGroup;
  validYear = new Date().getFullYear();

  /**
   * Creates an instance of MoviesModalComponent.
   * @param {MovieDTO} data
   * @param {MatDialogRef<MoviesModalComponent>} dialogRef
   * @param {FormBuilder} formBuilder
   * @param {UtilService} utilService
   * @param {ToastrService} toastr
   * @param {MovieService} movieService
   * @memberof MoviesModalComponent
   */
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: MovieDTO,
    public dialogRef: MatDialogRef<MoviesModalComponent>,
    private formBuilder: FormBuilder,
    public utilService: UtilService,
    private toastr: ToastrService,
    private movieService: MovieService
  ) {}

  /**
   * On Init hook
   * Determine what is current event
   * @memberof MoviesModalComponent
   */
  ngOnInit() {
    if (this.data !== null && this.data !== undefined) {
      this.title = 'Update';
      this.isUpdate = true;
    }
    this.initForm();
  }

  /**
   * Exists whithout any action
   * @memberof MoviesModalComponent
   */
  public exitWithoutAction(): void {
    this.dialogRef.close();
  }

  /**
   * Initalizes the form
   * If the current event is update, it will set form datas too
   * @memberof MoviesModalComponent
   */
  public initForm(): void {
    this.form = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.maxLength(999)]],
      year: [
        '',
        [
          Validators.min(1500),
          Validators.max(this.validYear),
          Validators.required
        ]
      ]
    });

    if (this.isUpdate) {
      this.form.setValue({
        title: this.data.title,
        description: this.data.description,
        year: this.data.year
      });
    }
  }

  /**
   * Saves the creation or the modification
   * @memberof MoviesModalComponent
   */
  public save(): void {
    // Check form
    if (this.form.invalid) {
      this.toastr.warning(this.utilService.formIsInvalidMessage());
    } else {
      const response: IModalResponse = {
        data: this.getFormValues(),
        isSuccess: true,
        needRefresh: true,
        isEdit: this.isUpdate
      };
      // Check current event
      if (this.isUpdate) {
        response.data.id = this.data.id;
        // Send update request
        this.movieService
          .updateMovie(response.data as MovieDTO)
          .then(() => {
            this.toastr.success('', this.utilService.updateSuccess);
            this.dialogRef.close(response);
          })
          .catch((err: HttpErrorResponse) => {
            this.toastr.error(err.message, this.utilService.updateFailed);
          });
      } else {
        // Send add request
        this.movieService
          .createMovie(response.data as MovieDTO)
          .then(() => {
            this.toastr.success('', this.utilService.createSuccess);
            this.dialogRef.close(response);
          })
          .catch((err: HttpErrorResponse) => {
            this.toastr.error(err.message, this.utilService.createFailed);
          });
      }
    }
  }

  /**
   * Gets form values
   * @returns {MovieDTO} Movie element from the form
   * @memberof MoviesModalComponent
   */
  public getFormValues(): MovieDTO {
    return {
      id: null,
      title: this.form.get('title').value,
      description: this.form.get('description').value,
      year: +this.form.get('year').value
    } as MovieDTO;
  }
}
