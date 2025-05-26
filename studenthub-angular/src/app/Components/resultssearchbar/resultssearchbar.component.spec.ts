import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultssearchbarComponent } from './resultssearchbar.component';

describe('ResultssearchbarComponent', () => {
  let component: ResultssearchbarComponent;
  let fixture: ComponentFixture<ResultssearchbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResultssearchbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultssearchbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
