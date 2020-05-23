import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SeriesListDTO, SeriesDTO, EpisodeDTO } from '../models';

@Injectable({
  providedIn: 'root'
})
export class SeriesService {
  private url = environment.api + '/series';

  constructor(private http: HttpClient) {}

  public getAllSeries(): Promise<SeriesListDTO[]> {
    return this.http.get<SeriesListDTO[]>(`${this.url}`).toPromise();
  }

  public getMySeries(): Promise<SeriesListDTO[]> {
    return this.http.get<SeriesListDTO[]>(`${this.url}`).toPromise();
  }

  public addEpisodesToSeason(
    seasonId: number,
    episodesNumbers: number[]
  ): Promise<void> {
    return this.http
      .post<void>(`${this.url}/season/${seasonId}/episodes`, {
        nums: episodesNumbers
      })
      .toPromise();
  }

  public addSeasonsToSeries(
    seriesId: number,
    seasonNumbers: number[]
  ): Promise<void> {
    return this.http
      .post<void>(`${this.url}/${seriesId}/seasons`, {
        nums: seasonNumbers
      })
      .toPromise();
  }

  public createSeries(series: SeriesDTO): Promise<void> {
    return this.http
      .post<void>(`${this.url}`, {
        series
      })
      .toPromise();
  }

  public deleteEpisodesFromSeason(episodeIds: number[]): Promise<void> {
    let params = new HttpParams();
    const stringArray: string[] = episodeIds.map(x => x.toString());
    params = params.append('episodeIds[]', stringArray.join(', '));
    return this.http
      .delete<void>(`${this.url}/season/episodes`, { params: params })
      .toPromise();
  }

  public deleteSeasonsFromSeries(seasonIds: number[]): Promise<void> {
    let params = new HttpParams();
    const stringArray: string[] = seasonIds.map(x => x.toString());
    params = params.append('seasonIds[]', stringArray.join(', '));
    return this.http
      .delete<void>(`${this.url}/seasons`, { params: params })
      .toPromise();
  }

  public deleteSeries(seriesId: number): Promise<void> {
    return this.http.delete<void>(`${this.url}/${seriesId}`).toPromise();
  }

  public updateEpisode(episodeId: number, episode: EpisodeDTO): Promise<void> {
    return this.http
      .put<void>(`${this.url}/season/episode/${episodeId}`, { episode })
      .toPromise();
  }

  public updateSeries(seriesId: number, series: SeriesDTO): Promise<void> {
    return this.http
      .put<void>(`${this.url}/${seriesId}`, { series })
      .toPromise();
  }
}
