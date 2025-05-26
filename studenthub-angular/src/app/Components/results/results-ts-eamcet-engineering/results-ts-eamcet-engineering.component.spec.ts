import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultsBieApComponent } from './results-bie-ap.component';

describe('ResultsBieApComponent', () => {
  let component: ResultsBieApComponent;
  let fixture: ComponentFixture<ResultsBieApComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResultsBieApComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultsBieApComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
