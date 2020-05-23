/**
 * ToDo Date group DTO
 * @export
 * @class ToDoDateDTO
 */
export class ToDoDateListDTO {
  dueDate: Date;
  toDoList: ToDoListDTO[];
}

/**
 * ToDo DTO
 * @export
 * @class ToDoDTO
 */
export class ToDoListDTO {
  id: number;
  title: string;
}

export class ToDoDTO {
  id: number;
  title: string;
  dueDate: Date;
}
