import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { PricingTransactionalComponent } from './pricing-transactional.component';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { BaseConcessionFilterPipe } from "../filters/base-concession-filter.pipe";
import { TransactionalConcessionService, MockTransactionalConcessionService } from "../services/transactional-concession.service";

describe('PricingTransactionalComponent', () => {
    let component: PricingTransactionalComponent;
    let fixture: ComponentFixture<PricingTransactionalComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, FormsModule, HttpModule],
            declarations: [PricingTransactionalComponent, BaseConcessionFilterPipe],
            providers: [{ provide: TransactionalConcessionService, useClass: MockTransactionalConcessionService }]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PricingTransactionalComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
