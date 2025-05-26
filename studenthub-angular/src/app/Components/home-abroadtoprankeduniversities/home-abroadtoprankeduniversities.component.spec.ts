import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeAbroadtoprankeduniversitiesComponent } from './home-abroadtoprankeduniversities.component';

describe('HomeAbroadtoprankeduniversitiesComponent', () => {
  let component: HomeAbroadtoprankeduniversitiesComponent;
  let fixture: ComponentFixture<HomeAbroadtoprankeduniversitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeAbroadtoprankeduniversitiesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeAbroadtoprankeduniversitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
