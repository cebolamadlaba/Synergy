import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormArray, FormBuilder, Validators, FormControl } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpModule } from '@angular/http';
import { ProvinceComponent } from './province.component';
import { ProvinceService, MockProvinceService } from "../../services/province.service";

describe('ProvinceComponent', () => {
  let component: ProvinceComponent;
  let fixture: ComponentFixture<ProvinceComponent>;

  beforeEach(async(() => {
      TestBed.configureTestingModule({
          imports: [HttpModule, FormsModule, ReactiveFormsModule, RouterTestingModule],
          declarations: [ProvinceComponent],
          providers: [{ provide: ProvinceService, useClass: MockProvinceService }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProvinceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
