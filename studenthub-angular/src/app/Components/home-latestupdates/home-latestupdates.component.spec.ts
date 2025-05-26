import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeLatestupdatesComponent } from './home-latestupdates.component';

describe('HomeLatestupdatesComponent', () => {
  let component: HomeLatestupdatesComponent;
  let fixture: ComponentFixture<HomeLatestupdatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeLatestupdatesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeLatestupdatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
