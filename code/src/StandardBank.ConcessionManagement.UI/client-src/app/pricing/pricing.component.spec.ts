import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PricingComponent } from './pricing.component';
import { UserService, MockUserService } from "../user/user.service";
import { RiskGroupLegalEntitiesService, MockRiskGroupLegalEntitiesService } from "../risk-group-legal-entities/risk-group-legal-entities.service";
import { FormsModule } from '@angular/forms';

describe('PricingComponent', () => {
    let component: PricingComponent;
    let fixture: ComponentFixture<PricingComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, FormsModule],
            declarations: [PricingComponent],
            providers: [
                { provide: UserService, useClass: MockUserService },
                { provide: RiskGroupLegalEntitiesService, useClass: MockRiskGroupLegalEntitiesService }
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
