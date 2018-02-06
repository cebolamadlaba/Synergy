import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AaManagementComponent } from './aa-management.component';

describe('AaManagementComponent', () => {
  let component: AaManagementComponent;
  let fixture: ComponentFixture<AaManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AaManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AaManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
