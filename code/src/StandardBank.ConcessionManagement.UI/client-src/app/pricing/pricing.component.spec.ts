import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { PricingComponent } from './pricing.component';
import { UserService, MockUserService } from "../services/user.service";
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";

describe('PricingComponent', () => {
    let component: PricingComponent;
    let fixture: ComponentFixture<PricingComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, FormsModule, RouterTestingModule],
            declarations: [PricingComponent],
            providers: [
                { provide: UserService, useClass: MockUserService },
                { provide: LookupDataService, useClass: MockLookupDataService }
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
