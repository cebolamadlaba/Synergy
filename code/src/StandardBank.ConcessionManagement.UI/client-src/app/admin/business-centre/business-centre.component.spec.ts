import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessCentreComponent } from './business-centre.component';

describe('BusinessCentreComponent', () => {
  let component: BusinessCentreComponent;
  let fixture: ComponentFixture<BusinessCentreComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusinessCentreComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessCentreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
