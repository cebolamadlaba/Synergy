import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DataTablesModule } from 'angular-datatables';
import { ConditionsComponent } from './conditions.component';
import { FormsModule } from '@angular/forms';
import { MyConditionService, MockMyConditionService } from "../services/my-condition.service";
import { PeriodService, MockPeriodService } from "../services/period.service";
import { HttpModule } from '@angular/http';

describe('ConditionsComponent', () => {
    let component: ConditionsComponent;
    let fixture: ComponentFixture<ConditionsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [DataTablesModule, FormsModule, HttpModule],
            declarations: [ConditionsComponent],
            providers: [
                { provide: MyConditionService, useClass: MockMyConditionService },
                { provide: PeriodService, useClass: MockPeriodService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ConditionsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
