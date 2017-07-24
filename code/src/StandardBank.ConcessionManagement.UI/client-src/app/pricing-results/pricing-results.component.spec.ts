import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PricingResultsComponent } from './pricing-results.component';
import { UserService, MockUserService } from "../user/user.service";

describe('PricingResultsComponent', () => {
    let component: PricingResultsComponent;
    let fixture: ComponentFixture<PricingResultsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            declarations: [PricingResultsComponent],
            providers: [{ provide: UserService, useClass: MockUserService }]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingResultsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
