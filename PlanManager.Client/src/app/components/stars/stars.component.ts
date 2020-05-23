import { Component, OnInit } from '@angular/core';
import { Star } from 'src/app/models/Star';

@Component({
  selector: 'pm-stars',
  templateUrl: './stars.component.html',
  styleUrls: ['./stars.component.scss'],
})
export class StarsComponent implements OnInit {
  public stars: Star[] = [];
  constructor() {}

  ngOnInit(): void {
    this.generateStars(500);
  }

  public generateStars(numberOfStars: number): void {
    let i = 0;
    while (i < numberOfStars) {
      this.stars.push({
        x: (window.innerHeight - 5) * Math.random(),
        y: (window.innerWidth - 5) * Math.random(),
        type: Math.floor(Math.random() * 3) + 1,
        lifeTime: Math.floor(Math.random() * 20) + 3,
      });
      i++;
    }
  }
}
