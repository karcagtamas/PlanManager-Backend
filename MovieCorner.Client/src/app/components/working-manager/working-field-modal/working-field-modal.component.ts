import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  UtilService,
  WorkingManagerService,
  NotificationService
} from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { WorkingDayListDTO, WorkingFieldDTO } from 'src/app/models';
import {
  IWorkingFieldModalData,
  IModalResponse,
  IConfirmData
} from 'src/app/interfaces';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'ks-working-field-modal',
  templateUrl: './working-field-modal.component.html',
  styleUrls: ['./working-field-modal.component.scss']
})
export class WorkingFieldModalComponent implements OnInit {
  title = 'Create';
  isUpdate = false;
  form: FormGroup;
  maxLength = 0;

  constructor(
    private dialogRef: MatDialogRef<WorkingFieldModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IWorkingFieldModalData,
    private formBuilder: FormBuilder,
    public utilService: UtilService,
    private toastr: ToastrService,
    private workingManagerService: WorkingManagerService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    if (
      this.data.workingField !== null &&
      this.data.workingField !== undefined
    ) {
      this.title = 'Update';
      this.isUpdate = true;
    }
    this.calculateMaxLength();
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
      title: ['', [Validators.required]],
      description: ['', []],
      length: [
        '',
        [Validators.required, Validators.min(0), Validators.max(this.maxLength)]
      ]
    });

    if (this.isUpdate) {
      this.form.setValue({
        title: this.data.workingField.title,
        description: this.data.workingField.description,
        length: this.data.workingField.length
      });
    }
  }

  private calculateMaxLength(): void {
    const startMin =
      this.data.workingDay.startHour * 60 + this.data.workingDay.startMin;
    const endMin =
      this.data.workingDay.endHour * 60 + this.data.workingDay.endMin;
    let max = (endMin - startMin) / 60;

    for (const i of this.data.workingDay.workingFields) {
      max -= i.length;
    }
    if (this.isUpdate) {
      max += this.data.workingField.length;
    }
    this.maxLength = max;
  }

  public save(): void {
    if (this.form.invalid) {
      this.toastr.warning(this.utilService.formIsInvalidMessage());
      return;
    }

    const response: IModalResponse = {
      data: this.getFormValues(),
      isSuccess: true,
      needRefresh: true,
      isEdit: true
    };

    if (this.isUpdate) {
      response.data.id = this.data.workingField.id;
      this.workingManagerService
        .updateWorkingField(response.data as WorkingFieldDTO)
        .then(() => {
          this.toastr.success('', this.utilService.updateSuccess);
          this.dialogRef.close(response);
        })
        .catch((err: HttpErrorResponse) => {
          this.toastr.error(err.message, this.utilService.updateFailed);
        });
    } else {
      this.workingManagerService
        .addWorkingField(
          this.data.workingDay.id,
          response.data as WorkingFieldDTO
        )
        .then(() => {
          this.toastr.success('', this.utilService.createSuccess);
          this.dialogRef.close(response);
        })
        .catch((err: HttpErrorResponse) => {
          this.toastr.error(err.message, this.utilService.createFailed);
        });
    }
  }

  public getFormValues(): WorkingFieldDTO {
    return {
      id: null,
      title: this.form.get('title').value,
      description: this.form.get('description').value,
      length: this.form.get('length').value
    } as WorkingFieldDTO;
  }

  public delete(field: WorkingFieldDTO): void {
    const data: IConfirmData = { title: 'WorkingField', name: field.title };
    this.notificationService.confirm(data).then(res => {
      if (res) {
        this.workingManagerService
          .deleteWorkingField(field.id)
          .then(() => {
            this.toastr.success('', this.utilService.deleteSuccess);
            const response: IModalResponse = {
              data: null,
              isSuccess: true,
              needRefresh: true,
              isEdit: true
            };
            this.dialogRef.close(response);
          })
          .catch((err: HttpErrorResponse) => {
            this.toastr.error(err.message, this.utilService.deleteFailed);
          });
      }
    });
  }
}
