import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostReflectionQuestionsComponent } from './post-reflection-questions.component';

describe('PostReflectionQuestionsComponent', () => {
  let component: PostReflectionQuestionsComponent;
  let fixture: ComponentFixture<PostReflectionQuestionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostReflectionQuestionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostReflectionQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
