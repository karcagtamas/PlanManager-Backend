import {
  MovieDTO,
  WorkingDayDTO,
  WorkingFieldDTO,
  SeriesDTO,
  ToDoDTO
} from '../models';

/**
 * General modal response data interface
 * @export
 * @interface IModalResponse
 */
export interface IModalResponse {
  isSuccess: boolean;
  isEdit: boolean;
  data: MovieDTO | WorkingDayDTO | WorkingFieldDTO | SeriesDTO | ToDoDTO;
  needRefresh: boolean;
}
