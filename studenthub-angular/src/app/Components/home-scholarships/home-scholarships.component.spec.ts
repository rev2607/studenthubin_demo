import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeScholarshipsComponent } from './home-scholarships.component';

describe('HomeScholarshipsComponent', () => {
  let component: HomeScholarshipsComponent;
  let fixture: ComponentFixture<HomeScholarshipsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeScholarshipsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeScholarshipsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
