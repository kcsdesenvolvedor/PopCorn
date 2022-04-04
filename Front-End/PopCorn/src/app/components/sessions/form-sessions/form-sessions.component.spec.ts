import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormSessionsComponent } from './form-sessions.component';

describe('FormSessionsComponent', () => {
  let component: FormSessionsComponent;
  let fixture: ComponentFixture<FormSessionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormSessionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormSessionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
