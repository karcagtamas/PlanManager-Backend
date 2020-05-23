import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PmPlanComponent } from './pm-plan.component';

describe('PmPlanComponent', () => {
  let component: PmPlanComponent;
  let fixture: ComponentFixture<PmPlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PmPlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PmPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
