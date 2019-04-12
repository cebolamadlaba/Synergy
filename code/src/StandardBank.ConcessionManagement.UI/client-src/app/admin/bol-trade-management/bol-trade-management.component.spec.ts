import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BolTradeManagementComponent } from './bol-trade-management.component';

describe('BolTradeManagementComponent', () => {
  let component: BolTradeManagementComponent;
  let fixture: ComponentFixture<BolTradeManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BolTradeManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BolTradeManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
