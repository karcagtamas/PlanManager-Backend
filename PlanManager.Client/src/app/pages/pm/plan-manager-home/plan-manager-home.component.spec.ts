import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanManagerHomeComponent } from './plan-manager-home.component';

describe('PlanManagerHomeComponent', () => {
  let component: PlanManagerHomeComponent;
  let fixture: ComponentFixture<PlanManagerHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlanManagerHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanManagerHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
