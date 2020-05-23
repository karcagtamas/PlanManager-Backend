import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmHomeComponent } from './em-home.component';

describe('EmHomeComponent', () => {
  let component: EmHomeComponent;
  let fixture: ComponentFixture<EmHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
