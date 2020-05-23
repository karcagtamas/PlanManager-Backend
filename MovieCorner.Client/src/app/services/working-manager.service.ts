import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  WorkingDayListDTO,
  WorkingFieldDTO,
  WorkingDayTypeDTO,
  WorkingDayDTO
} from '../models';
import { environment } from 'src/environments/environment';
import { UtilService } from './util.service';

@Injectable({
  providedIn: 'root'
})
export class WorkingManagerService {
  private url = environment.api + '/workingmanager';

  constructor(private http: HttpClient, private utilService: UtilService) {}

  public getWorkingDay(day: Date): Promise<WorkingDayListDTO> {
    return this.http
      .get<WorkingDayListDTO>(
        `${this.url}/${this.utilService.toDateString(day)}`
      )
      .toPromise();
  }

  public createWorkingDay(workingDay: WorkingDayDTO): Promise<void> {
    return this.http.post<void>(`${this.url}`, workingDay).toPromise();
  }

  public updateWorkingDay(workingDay: WorkingDayDTO): Promise<void> {
    return this.http
      .put<void>(`${this.url}/${workingDay.id}`, workingDay)
      .toPromise();
  }

  public addWorkingField(
    workingDayId: number,
    workingField: WorkingFieldDTO
  ): Promise<void> {
    return this.http
      .post<void>(`${this.url}/${workingDayId}/field`, workingField)
      .toPromise();
  }

  public deleteWorkingField(workingFieldId: number): Promise<void> {
    return this.http
      .delete<void>(`${this.url}/field/${workingFieldId}`)
      .toPromise();
  }

  public updateWorkingField(workingField: WorkingFieldDTO): Promise<void> {
    return this.http
      .put<void>(`${this.url}/field/${workingField.id}`, workingField)
      .toPromise();
  }

  public getWorkingDayTypes(): Promise<WorkingDayTypeDTO[]> {
    return this.http.get<WorkingDayTypeDTO[]>(`${this.url}/types`).toPromise();
  }
}
