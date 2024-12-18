import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmuLoaderComponent } from './smu-loader.component';

describe('SmuLoaderComponent', () => {
  let component: SmuLoaderComponent;
  let fixture: ComponentFixture<SmuLoaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmuLoaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmuLoaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
