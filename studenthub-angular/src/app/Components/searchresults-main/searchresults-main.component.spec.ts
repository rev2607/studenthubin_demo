import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchresultsMainComponent } from './searchresults-main.component';

describe('SearchresultsMainComponent', () => {
  let component: SearchresultsMainComponent;
  let fixture: ComponentFixture<SearchresultsMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchresultsMainComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchresultsMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
