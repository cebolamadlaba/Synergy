import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BolAddConcessionComponent } from './bol-add-concession.component';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";

describe('BolAddConcessionComponent', () => {
    let component: BolAddConcessionComponent;
    let fixture: ComponentFixture<BolAddConcessionComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
            declarations: [BolAddConcessionComponent],
            providers: [
                { provide: LookupDataService, useClass: MockLookupDataService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(BolAddConcessionComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
