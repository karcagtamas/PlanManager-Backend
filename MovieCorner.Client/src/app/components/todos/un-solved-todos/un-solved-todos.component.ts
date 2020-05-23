import { Component, OnInit } from '@angular/core';
import { ToDoDateListDTO, ToDoListDTO, ToDoDTO } from 'src/app/models';
import {
  TodoService,
  NotificationService,
  UtilService
} from 'src/app/services';
import { MatDialog } from '@angular/material/dialog';
import { TodoModalComponent } from '../todo-modal/todo-modal.component';
import { IModalResponse, IConfirmData } from 'src/app/interfaces';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

/**
 * Unsolved ToDos Compoenent
 * Displays user's all unsolved todos
 * @export
 * @class UnSolvedTodosComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-un-solved-todos',
  templateUrl: './un-solved-todos.component.html',
  styleUrls: ['./un-solved-todos.component.scss']
})
export class UnSolvedTodosComponent implements OnInit {
  todoDateDTOs: ToDoDateListDTO[] = [];

  /**
   * Creates an instance of UnSolvedTodosComponent.
   * @memberof UnSolvedTodosComponent
   */
  constructor(
    private todoService: TodoService,
    private dialog: MatDialog,
    private notificationService: NotificationService,
    private toastr: ToastrService,
    private utilService: UtilService
  ) {}

  /**
   * On Init hook
   * Gets todos
   * @memberof UnSolvedTodosComponent
   */
  ngOnInit() {
    this.getTodoDates();
  }

  /**
   * Gets all todos as groupped
   * @memberof UnSolvedTodosComponent
   */
  public getTodoDates(): void {
    this.todoService
      .getTodos(false)
      .then(res => {
        this.todoDateDTOs = res;
      })
      .catch(() => {
        this.todoDateDTOs = [];
      });
  }

  public createTodo(): void {
    this.openDialog(null);
  }

  public updateTodo(todo: ToDoListDTO): void {
    this.todoService.getTodo(todo.id).then(res => {
      this.openDialog(res);
    });
  }

  public deleteTodo(todo: ToDoDTO): void {
    const data: IConfirmData = { title: 'ToDo', name: todo.title };
    this.notificationService.confirm(data).then(res => {
      if (res) {
        this.todoService
          .deleteTodo(todo.id)
          .then(() => {
            this.toastr.success('', this.utilService.deleteSuccess);
            this.getTodoDates();
          })
          .catch((err: HttpErrorResponse) => {
            this.toastr.error(err.message, this.utilService.deleteFailed);
          });
      }
    });
  }

  public openDialog(todo: ToDoDTO): void {
    const dialogRef = this.dialog.open(TodoModalComponent, {
      data: todo,
      minWidth: '80%',
      minHeight: '80%'
    });

    dialogRef.afterClosed().subscribe((res: IModalResponse) => {
      if (res && res.needRefresh) {
        this.getTodoDates();
      }
    });
  }
}
