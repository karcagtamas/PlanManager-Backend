import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MovieDTO, SeriesDTO } from 'src/app/models';
import { UtilService, MovieService, SeriesService } from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { IModalResponse } from 'src/app/interfaces';

@Component({
  selector: 'ks-series-modal',
  templateUrl: './series-modal.component.html',
  styleUrls: ['./series-modal.component.scss']
})
export class SeriesModalComponent implements OnInit {
  title = 'Create';
  isUpdate = false;
  form: FormGroup;
  validYear = new Date().getFullYear();

  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: SeriesDTO,
    public dialogRef: MatDialogRef<SeriesModalComponent>,
    private formBuilder: FormBuilder,
    public utilService: UtilService,
    private toastr: ToastrService,
    private seriesService: SeriesService
  ) {}

  /**
   * On Init hook
   * Determine what is current event
   * @memberof SeriesModalComponent
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
   * @memberof SeriesModalComponent
   */
  public exitWithoutAction(): void {
    this.dialogRef.close();
  }

  /**
   * Initalizes the form
   * If the current event is update, it will set form datas too
   * @memberof SeriesModalComponent
   */
  public initForm(): void {
    this.form = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.maxLength(999)]],
      startYear: ['', [Validators.min(1500), Validators.max(this.validYear)]],
      endYear: ['', [Validators.min(1500), Validators.max(this.validYear)]]
    });

    if (this.isUpdate) {
      this.form.setValue({
        title: this.data.title,
        description: this.data.description,
        startYear: this.data.startYear,
        endYear: this.data.endYear
      });
    }
  }

  /**
   * Saves the creation or the modification
   * @memberof SeriesModalComponent
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
        this.seriesService
          .updateSeries(this.data.id, response.data as SeriesDTO)
          .then(() => {
            this.toastr.success('', this.utilService.updateSuccess);
            this.dialogRef.close(response);
          })
          .catch((err: HttpErrorResponse) => {
            this.toastr.error(err.message, this.utilService.updateFailed);
          });
      } else {
        // Send add request
        this.seriesService
          .createSeries(response.data as SeriesDTO)
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
   * @returns {SeriesDTO} Series element from the form
   * @memberof SeriesModalComponent
   */
  public getFormValues(): SeriesDTO {
    return {
      id: null,
      title: this.form.get('title').value,
      description: this.form.get('description').value
        ? this.form.get('description').value
        : null,
      startYear: this.form.get('startYear').value
        ? +this.form.get('startYear').value
        : null,
      endYear: this.form.get('endYear').value
        ? +this.form.get('endYear').value
        : null
    } as SeriesDTO;
  }
}
