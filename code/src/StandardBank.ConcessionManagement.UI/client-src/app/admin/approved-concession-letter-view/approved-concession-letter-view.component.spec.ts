import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovedConcessionLetterViewComponent } from './approved-concession-letter-view.component';

describe('ApprovedConcessionLetterViewComponent', () => {
  let component: ApprovedConcessionLetterViewComponent;
  let fixture: ComponentFixture<ApprovedConcessionLetterViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprovedConcessionLetterViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovedConcessionLetterViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
