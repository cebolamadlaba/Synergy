import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PricingComponent } from './pricing.component';
import { UserService, MockUserService } from "../user/user.service";
import { FormsModule } from '@angular/forms';
import { RiskGroupNameService, MockRiskGroupNameService } from "../risk-group-name/risk-group-name.service";

describe('PricingComponent', () => {
    let component: PricingComponent;
    let fixture: ComponentFixture<PricingComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, FormsModule],
            declarations: [PricingComponent],
            providers: [
                { provide: UserService, useClass: MockUserService },
                { provide: RiskGroupNameService, useClass: MockRiskGroupNameService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
