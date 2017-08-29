import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BolAddConcessionComponent } from './bol-add-concession.component';

describe('BolAddConcessionComponent', () => {
  let component: BolAddConcessionComponent;
  let fixture: ComponentFixture<BolAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BolAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BolAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
