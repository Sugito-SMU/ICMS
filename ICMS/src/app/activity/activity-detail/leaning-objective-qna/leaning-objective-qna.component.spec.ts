import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaningObjectiveQnaComponent } from './leaning-objective-qna.component';

describe('LeaningObjectiveQnaComponent', () => {
  let component: LeaningObjectiveQnaComponent;
  let fixture: ComponentFixture<LeaningObjectiveQnaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaningObjectiveQnaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaningObjectiveQnaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
