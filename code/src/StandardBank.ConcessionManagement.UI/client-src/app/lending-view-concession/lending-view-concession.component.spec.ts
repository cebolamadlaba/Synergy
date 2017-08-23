import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LendingViewConcessionComponent } from './lending-view-concession.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LendingService, MockLendingService } from "../services/lending.service";
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";

describe('LendingViewConcessionComponent', () => {
    let component: LendingViewConcessionComponent;
    let fixture: ComponentFixture<LendingViewConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [LendingViewConcessionComponent],
            providers: [
                { provide: LookupDataService, useClass: MockLookupDataService },
                { provide: LendingService, useClass: MockLendingService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LendingViewConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', function (done) {
        expect(component).toBeTruthy();
        done();
    });
});
