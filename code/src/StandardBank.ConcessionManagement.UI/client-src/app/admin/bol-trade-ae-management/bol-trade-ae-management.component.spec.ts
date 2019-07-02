import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BolTradeAeManagementComponent } from './bol-trade-ae-management.component';

describe('BolTradeAeManagementComponent', () => {
  let component: BolTradeAeManagementComponent;
  let fixture: ComponentFixture<BolTradeAeManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BolTradeAeManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BolTradeAeManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
