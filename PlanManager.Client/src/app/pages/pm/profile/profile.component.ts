import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'pm-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public roleNumbers: string;
  public user: User = {
    id: '1',
    userName: 'barni363hun',
    email: 'barni.pbs@gmail.com',
    secondaryEmail: 'barni363hun@gmail.com',
    fullName: 'Princzes Barnabás ',
    registrationDate: new Date(),
    lastLogin: new Date(),
    roles: ['Developer', 'User'],
    phone: 202790962,
  };

  constructor() {}

  ngOnInit(): void {
    if (this.user.roles.length > 1) {
      this.roleNumbers = 'levels';
    } else {
      this.roleNumbers = 'level';
    }
  }
}
