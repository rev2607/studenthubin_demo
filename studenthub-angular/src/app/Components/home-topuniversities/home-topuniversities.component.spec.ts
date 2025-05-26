import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeTopuniversitiesComponent } from './home-topuniversities.component';

describe('HomeTopuniversitiesComponent', () => {
  let component: HomeTopuniversitiesComponent;
  let fixture: ComponentFixture<HomeTopuniversitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeTopuniversitiesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeTopuniversitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
