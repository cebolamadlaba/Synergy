import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LookupDataService, MockLookupDataService } from "../services/lookup-data.service";
import { CashmanAddConcessionComponent } from './cashman-add-concession.component';

describe('CashmanAddConcessionComponent', () => {
  let component: CashmanAddConcessionComponent;
  let fixture: ComponentFixture<CashmanAddConcessionComponent>;

  beforeEach(async(() => {
      TestBed.configureTestingModule({
          imports: [HttpModule, ModalModule.forRoot(), RouterTestingModule, FormsModule, ReactiveFormsModule],
          declarations: [CashmanAddConcessionComponent],
          providers: [
              { provide: LookupDataService, useClass: MockLookupDataService }
          ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CashmanAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
