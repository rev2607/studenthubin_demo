import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultdetailsComponent } from './resultdetails.component';

describe('ResultdetailsComponent', () => {
  let component: ResultdetailsComponent;
  let fixture: ComponentFixture<ResultdetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResultdetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultdetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
