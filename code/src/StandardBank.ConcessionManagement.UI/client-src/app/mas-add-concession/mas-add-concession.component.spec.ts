import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MasAddConcessionComponent } from './mas-add-concession.component';

describe('MasAddConcessionComponent', () => {
  let component: MasAddConcessionComponent;
  let fixture: ComponentFixture<MasAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
