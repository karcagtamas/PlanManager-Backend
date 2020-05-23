import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MovieListDTO, MovieDTO, OwnMovieDTO } from '../models';
import { environment } from 'src/environments/environment';

/**
 * Movie Service for managinn movies
 * @export
 * @class MovieService
 */
@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private url = environment.api + '/movie';

  /**
   * Creates an instance of MovieService.
   * @param {HttpClient} http
   * @memberof MovieService
   */
  constructor(private http: HttpClient) {}

  /**
   * Gets all movies
   * @returns {Promise<MovieListDTO[]>} List of movies as Promise
   * @memberof MovieService
   */
  public getMovies(): Promise<MovieListDTO[]> {
    return this.http.get<MovieListDTO[]>(`${this.url}`).toPromise();
  }

  /**
   * Gets movie by Id
   * @param {number} id Movie Id
   * @returns {Promise<MovieDTO>} Movie as Promise
   * @memberof MovieService
   */
  public getMovie(id: number): Promise<MovieDTO> {
    return this.http.get<MovieDTO>(`${this.url}/${id}`).toPromise();
  }

  /**
   * Get own movies for teh current logged user
   * @returns {Promise<OwnMovieDTO[]>} List of own movies as Promise
   * @memberof MovieService
   */
  public getOwnMovies(): Promise<OwnMovieDTO[]> {
    return this.http.get<OwnMovieDTO[]>(`${this.url}/my`).toPromise();
  }

  /**
   * Create movie
   * @param {MovieDTO} movie New movie element
   * @returns {Promise<void>} Void response
   * @memberof MovieService
   */
  public createMovie(movie: MovieDTO): Promise<void> {
    return this.http.post<void>(`${this.url}`, movie).toPromise();
  }

  /**
   * Update an existing movie
   * @param {MovieDTO} movie Updated movie element
   * @returns {Promise<void>} Void response
   * @memberof MovieService
   */
  public updateMovie(movie: MovieDTO): Promise<void> {
    return this.http.put<void>(`${this.url}/${movie.id}`, movie).toPromise();
  }

  /**
   * Delete an existing movie by Id
   * @param {number} movieId Movie's Id
   * @returns {Promise<void>} Void response
   * @memberof MovieService
   */
  public deleteMovie(movieId: number): Promise<void> {
    return this.http.delete<void>(`${this.url}/${movieId}`).toPromise();
  }

  /**
   * Update mapping between current user and movies by the given movie list
   * @param {MovieListDTO[]} list Updated list of mapped movies
   * @returns {Promise<void>} Void response
   * @memberof MovieService
   */
  public updateMovieMappings(list: MovieListDTO[]): Promise<void> {
    return this.http.put<void>(`${this.url}/map`, list).toPromise();
  }

  /**
   * Update seen status for a mapping betwwen the current user and the given movie
   * @param {number} movieId Movie's Id
   * @param {boolean} newStatus New seen status
   * @returns {Promise<void>} Void response
   * @memberof MovieService
   */
  public updateSeenStatus(movieId: number, newStatus: boolean): Promise<void> {
    return this.http
      .post<void>(`${this.url}/map/status/${movieId}`, { seen: newStatus })
      .toPromise();
  }
}
