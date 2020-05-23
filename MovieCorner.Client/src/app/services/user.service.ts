import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ILoginDatas, ILoginResponse } from '../interfaces';
import { IRegistrationDatas } from '../interfaces/IRegistrationDatas';
import { IRegistrationResponse } from '../interfaces/IRegistrationResponse';
import { UserProfileDTO, UserProfileUpdateDTO } from '../models';

/**
 * User Service for managing users
 * @export
 * @class UserService
 */
@Injectable({
  providedIn: 'root'
})
export class UserService {
  url: string = environment.api + '/user';

  /**
   * Creates an instance of UserService.
   * @param {HttpClient} http
   * @memberof UserService
   */
  constructor(private http: HttpClient) {}

  /**
   * Managing login
   * @param {ILoginDatas} datas Login datas
   * @returns {Promise<ILoginResponse>} Result of the login request as Promise
   * @memberof UserService
   */
  public login(datas: ILoginDatas): Promise<ILoginResponse> {
    return this.http
      .post<ILoginResponse>(`${this.url}/login`, { ...datas })
      .toPromise();
  }

  /**
   * Get logged user datas
   * @returns {Promise<UserProfileDTO>} User Profile datas as Promise
   * @memberof UserService
   */
  public getUser(): Promise<UserProfileDTO> {
    return this.http.get<UserProfileDTO>(`${this.url}`).toPromise();
  }

  /**
   * Update user settings, datas
   * @param {UserProfileUpdateDTO} value Updated user profile datas
   * @returns {Promise<void>} Void response
   * @memberof UserService
   */
  public updateUser(value: UserProfileUpdateDTO): Promise<void> {
    return this.http.put<void>(`${this.url}/${value.id}`, value).toPromise();
  }

  /**
   * Managing registration
   * @param {IRegistrationDatas} datas Registration datas
   * @returns {Promise<IRegistrationResponse>} Result of the registration request as Promise
   * @memberof UserService
   */
  public registration(
    datas: IRegistrationDatas
  ): Promise<IRegistrationResponse> {
    return this.http
      .post<IRegistrationResponse>(`${this.url}/regist`, { ...datas })
      .toPromise();
  }
}
