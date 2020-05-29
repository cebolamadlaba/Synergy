import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LendingTierRateModalComponent } from './lending-tier-rate-modal.component';

describe('LendingTierRateModalComponent', () => {
  let component: LendingTierRateModalComponent;
  let fixture: ComponentFixture<LendingTierRateModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LendingTierRateModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LendingTierRateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
