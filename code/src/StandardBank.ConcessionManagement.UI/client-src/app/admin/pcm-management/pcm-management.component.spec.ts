import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PcmManagementComponent } from './pcm-management.component';

describe('PcmManagementComponent', () => {
  let component: PcmManagementComponent;
  let fixture: ComponentFixture<PcmManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PcmManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PcmManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
