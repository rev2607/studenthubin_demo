import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuickinfoComponent } from './quickinfo.component';

describe('QuickinfoComponent', () => {
  let component: QuickinfoComponent;
  let fixture: ComponentFixture<QuickinfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuickinfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuickinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
