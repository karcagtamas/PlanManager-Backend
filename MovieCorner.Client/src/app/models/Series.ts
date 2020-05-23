export class SeriesListDTO {
  id: number;
  title: string;
  description?: string;
  startYear?: number;
  endYear?: number;
  creationTime: Date;
  creater: string;
  seasons: SeasonListDTO[];
}

export class SeasonListDTO {
  id: number;
  number: number;
  episodes: EpisodeListDTO[];
}

export class EpisodeListDTO {
  id: number;
  number: number;
  description?: string;
}

export class SeriesDTO {
  id: number;
  title: string;
  description?: string;
  startYear?: number;
  endYear?: number;
}

export class EpisodeDTO {
  id: number;
  number: number;
  description?: string;
}
