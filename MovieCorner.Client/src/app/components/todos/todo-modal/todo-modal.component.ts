import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToDoDTO } from 'src/app/models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UtilService, TodoService } from 'src/app/services';
import { ToastrService } from 'ngx-toastr';
import { IModalResponse } from 'src/app/interfaces';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'ks-todo-modal',
  templateUrl: './todo-modal.component.html',
  styleUrls: ['./todo-modal.component.scss']
})
export class TodoModalComponent implements OnInit {
  title = 'Create';
  isUpdate = false;
  form: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<TodoModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ToDoDTO,
    private formBuilder: FormBuilder,
    public utilService: UtilService,
    private toastr: ToastrService,
    private todoService: TodoService
  ) {}

  ngOnInit(): void {
    if (this.data !== null && this.data !== undefined) {
      this.title = 'Update';
      this.isUpdate = true;
    }
    this.initForm();
  }

  public exitWithoutAction(): void {
    this.dialogRef.close();
  }

  public initForm(): void {
    this.form = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      dueDate: ['', [Validators.required]]
    });

    if (this.isUpdate) {
      this.form.setValue({
        title: this.data.title,
        dueDate: this.data.dueDate
      });
    }
  }

  public save(): void {
    if (this.form.invalid) {
      this.toastr.warning(this.utilService.formIsInvalidMessage());
    } else {
      const response: IModalResponse = {
        data: this.getFormValues(),
        isSuccess: true,
        needRefresh: true,
        isEdit: this.isUpdate
      };

      if (this.isUpdate) {
        response.data.id = this.data.id;

        this.todoService
          .updateTodo(this.data.id, response.data as ToDoDTO)
          .then(() => {
            this.toastr.success('', this.utilService.updateSuccess);
            this.dialogRef.close(response);
          })
          .catch((err: HttpErrorResponse) => {
            this.toastr.error(err.message, this.utilService.updateFailed);
          });
      } else {
        this.todoService
          .createTodo(response.data as ToDoDTO)
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

  public getFormValues(): ToDoDTO {
    return {
      id: null,
      title: this.form.get('title').value,
      dueDate: this.form.get('dueDate').value
    } as ToDoDTO;
  }
}
