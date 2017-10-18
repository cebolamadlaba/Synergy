import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DataTablesModule } from 'angular-datatables';
import { ConditionsComponent } from './conditions.component';
import { FormsModule } from '@angular/forms';
import { MyConditionService, MockMyConditionService } from "../services/my-condition.service";
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";
import { HttpModule } from '@angular/http';
import { ConditionsFilterPipe } from "../filters/conditions-filter.pipe";
import { RouterTestingModule } from '@angular/router/testing';

describe('ConditionsComponent', () => {
    let component: ConditionsComponent;
    let fixture: ComponentFixture<ConditionsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [DataTablesModule, FormsModule, HttpModule, RouterTestingModule],
            declarations: [ConditionsComponent, ConditionsFilterPipe],
            providers: [
                { provide: MyConditionService, useClass: MockMyConditionService },
                { provide: LookupDataService, useClass: MockLookupDataService }
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
