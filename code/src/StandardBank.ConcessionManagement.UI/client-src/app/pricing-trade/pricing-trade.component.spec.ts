import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";
import { PricingTradeComponent } from './pricing-trade.component';
import { UserService, MockUserService } from '../services/user.service';

describe('PricingTradeComponent', () => {
  let component: PricingTradeComponent;
  let fixture: ComponentFixture<PricingTradeComponent>;

  beforeEach(async(() => {
      TestBed.configureTestingModule({
          imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
          providers: [
              { provide: LookupDataService, useClass: MockLookupDataService }, { provide: UserService, useClass: MockUserService }
          ],
      declarations: [ PricingTradeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingTradeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
