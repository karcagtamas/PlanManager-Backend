/**
 * User profile DTO
 * @export
 * @class UserProfileDTO
 */
export class UserProfileDTO {
  id: string;
  userName: string;
  email: string;
  fullName: string;
  allergy: string;
  class: string;
  tShirtSize: string;
  lastLogin?: Date;
  registrationDate?: Date;
}

/**
 * User profile update DTO
 * @export
 * @class UserProfileUpdateDTO
 */
export class UserProfileUpdateDTO {
  id?: string;
  fullName: string;
  allergy: string;
  class: string;
  tShirtSize: string;
}
