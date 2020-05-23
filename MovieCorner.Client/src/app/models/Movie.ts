/**
 * Movie list DTO
 * @export
 * @class MovieListDTO
 */
export class MovieListDTO {
  id: number;
  title: string;
  year: number;
  creater: string;
  description: string;
}

/**
 * Movie DTO
 * @export
 * @class MovieDTO
 */
export class MovieDTO {
  id: number;
  title: string;
  description: string;
  year: number;
}

/**
 * Own Movie DTO
 * @export
 * @class OwnMovieDTO
 */
export class OwnMovieDTO {
  id: number;
  title: string;
  description: string;
  year: number;
  seen: boolean;
}
