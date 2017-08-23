import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LendingAddConcessionComponent } from './lending-add-concession.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";
import { LendingService, MockLendingService } from "../services/lending.service";

describe('LendingAddConcessionComponent', () => {
    let component: LendingAddConcessionComponent;
    let fixture: ComponentFixture<LendingAddConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [LendingAddConcessionComponent],
            providers: [
                { provide: LookupDataService, useClass: MockLookupDataService },
                { provide: LendingService, useClass: MockLendingService }
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LendingAddConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', function(done) {
        expect(component).toBeTruthy();
        done();
    });
});
