import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeQuestionsinterviewsloansComponent } from './home-questionsinterviewsloans.component';

describe('HomeQuestionsinterviewsloansComponent', () => {
  let component: HomeQuestionsinterviewsloansComponent;
  let fixture: ComponentFixture<HomeQuestionsinterviewsloansComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeQuestionsinterviewsloansComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeQuestionsinterviewsloansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
