import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeNewsresourcesComponent } from './home-newsresources.component';

describe('HomeNewsresourcesComponent', () => {
  let component: HomeNewsresourcesComponent;
  let fixture: ComponentFixture<HomeNewsresourcesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeNewsresourcesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeNewsresourcesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
