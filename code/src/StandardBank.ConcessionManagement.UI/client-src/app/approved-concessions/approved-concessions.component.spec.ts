import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovedConcessionsComponent } from './approved-concessions.component';

describe('ApprovedConcessionsComponent', () => {
  let component: ApprovedConcessionsComponent;
  let fixture: ComponentFixture<ApprovedConcessionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprovedConcessionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovedConcessionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
