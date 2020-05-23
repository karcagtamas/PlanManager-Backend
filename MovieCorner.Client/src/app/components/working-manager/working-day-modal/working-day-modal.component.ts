import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  WorkingDayListDTO,
  WorkingDayTypeDTO,
  WorkingDayDTO
} from 'src/app/models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UtilService } from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { IWorkingDayModalData } from 'src/app/interfaces/IWorkingDayModalData';
import { IModalResponse } from 'src/app/interfaces';
import { WorkingManagerService } from 'src/app/services/working-manager.service';
import { HttpErrorResponse } from '@angular/common/http';
import { min } from 'rxjs/operators';

@Component({
  selector: 'ks-working-day-modal',
  templateUrl: './working-day-modal.component.html',
  styleUrls: ['./working-day-modal.component.scss']
})
export class WorkingDayModalComponent implements OnInit {
  title = 'Create';
  isUpdate = false;
  form: FormGroup;
  workingDayTypes: WorkingDayTypeDTO[] = [];

  constructor(
    private dialogRef: MatDialogRef<WorkingDayModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IWorkingDayModalData,
    private formBuilder: FormBuilder,
    public utilService: UtilService,
    private toastr: ToastrService,
    private workingManagerService: WorkingManagerService
  ) {}

  ngOnInit() {
    this.getWorkingDayTypes();
    if (this.data.workingDay !== null && this.data.workingDay !== undefined) {
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

  public getWorkingDayTypes() {
    this.workingManagerService
      .getWorkingDayTypes()
      .then(res => {
        this.workingDayTypes = res;
      })
      .catch(err => {
        this.workingDayTypes = [];
      });
  }

  /**
   * Initalizes the form
   * If the current event is update, it will set form datas too
   * @memberof MoviesModalComponent
   */
  public initForm(): void {
    this.form = this.formBuilder.group({
      startHour: [
        '',
        [Validators.required, Validators.min(0), Validators.max(23)]
      ],
      startMin: [
        '',
        [Validators.required, Validators.min(0), Validators.max(59)]
      ],
      endHour: [
        '',
        [Validators.required, Validators.min(0), Validators.max(23)]
      ],
      endMin: [
        '',
        [Validators.required, Validators.min(0), Validators.max(59)]
      ],
      type: ['', [Validators.required]]
    });

    if (this.isUpdate) {
      this.form.setValue({
        startHour: this.data.workingDay.startHour,
        startMin: this.data.workingDay.startMin,
        endHour: this.data.workingDay.endHour,
        endMin: this.data.workingDay.endMin,
        type: this.data.workingDay.type.id
      });
    }
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
      const data: WorkingDayDTO = response.data as WorkingDayDTO;

      const start = data.startHour * 60 + data.startMin;
      const end = data.endHour * 60 + data.endMin;
      const current = (end - start) / 60;
      console.log(current);
      let minimum = 0;
      for (const i of this.data.workingDay.workingFields) {
        minimum += i.length;
      }

      if (current < minimum) {
        this.toastr.warning(
          this.utilService.invalidLengthIntervall(minimum, current)
        );
        return;
      }
    }

    if (this.isUpdate) {
      response.data.id = this.data.workingDay.id;
      this.workingManagerService
        .updateWorkingDay(response.data as WorkingDayDTO)
        .then(() => {
          this.toastr.success('', this.utilService.updateSuccess);
          this.dialogRef.close(response);
        })
        .catch((err: HttpErrorResponse) => {
          this.toastr.error(err.message, this.utilService.updateFailed);
        });
    } else {
      this.workingManagerService
        .createWorkingDay(response.data as WorkingDayDTO)
        .then(() => {
          this.toastr.success('', this.utilService.createSuccess);
          this.dialogRef.close(response);
        })
        .catch((err: HttpErrorResponse) => {
          this.toastr.error(err.message, this.utilService.createFailed);
        });
    }
  }

  public getFormValues(): WorkingDayDTO {
    return {
      id: null,
      startHour: this.form.get('startHour').value,
      startMin: this.form.get('startMin').value,
      endHour: this.form.get('endHour').value,
      endMin: this.form.get('endMin').value,
      day: this.data.day,
      type: this.form.get('type').value
    } as WorkingDayDTO;
  }
}
