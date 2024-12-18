import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreReflectionComponent } from './pre-reflection.component';

describe('PreReflectionComponent', () => {
  let component: PreReflectionComponent;
  let fixture: ComponentFixture<PreReflectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreReflectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreReflectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
