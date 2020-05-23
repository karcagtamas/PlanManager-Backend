import { Component, OnInit, Input } from '@angular/core';
import { Star } from 'src/app/models/Star';

@Component({
  selector: 'pm-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.scss'],
})
export class StarComponent implements OnInit {
  @Input() star: Star;
  public style: string;
  public type: string;
  public top: number;
  public left: number;
  public animation: string;

  constructor() {}

  ngOnInit(): void {
    this.top = this.star.x;
    this.left = this.star.y;
    this.animation = `twinkle_${this.star.type} ${this.star.lifeTime}s ease-in-out
    infinite`;
    this.type = `star_${this.star.type}`;
  }
}
