import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PmPlansComponent } from './pm-plans.component';

describe('PmPlansComponent', () => {
  let component: PmPlansComponent;
  let fixture: ComponentFixture<PmPlansComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PmPlansComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PmPlansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
