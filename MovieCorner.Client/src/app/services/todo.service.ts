import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ToDoDateListDTO, ToDoDTO } from '../models';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private url = environment.api + '/todo';

  constructor(private http: HttpClient) {}

  public getTodos(isSolved: boolean): Promise<ToDoDateListDTO[]> {
    const params = new HttpParams().set('isSolved', isSolved.toString());
    return this.http
      .get<ToDoDateListDTO[]>(`${this.url}`, { params })
      .toPromise();
  }

  public getTodo(todoId: number): Promise<ToDoDTO> {
    return this.http.get<ToDoDTO>(`${this.url}/${todoId}`).toPromise();
  }

  public createTodo(todo: ToDoDTO): Promise<void> {
    return this.http.post<void>(`${this.url}`, todo).toPromise();
  }

  public updateTodo(todoId: number, todo: ToDoDTO): Promise<void> {
    return this.http.put<void>(`${this.url}/${todoId}`, todo).toPromise();
  }

  public deleteTodo(todoId: number): Promise<void> {
    return this.http.delete<void>(`${this.url}/${todoId}`).toPromise();
  }
}
