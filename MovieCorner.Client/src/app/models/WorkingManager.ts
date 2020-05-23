export class WorkingFieldDTO {
  id: number;
  title: string;
  description: string;
  length: number;
}

export class WorkingDayListDTO {
  id: number;
  day: Date;
  startHour: number;
  startMin: number;
  endHour: number;
  endMin: number;
  type: WorkingDayTypeDTO;
  workingFields: WorkingFieldDTO[];
}

export class WorkingDayDTO {
  id: number;
  day: Date;
  startHour: number;
  startMin: number;
  endHour: number;
  endMin: number;
  type: number;
}

export class WorkingDayTypeDTO {
  id: number;
  title: string;
  DayIsActive: boolean;
}
