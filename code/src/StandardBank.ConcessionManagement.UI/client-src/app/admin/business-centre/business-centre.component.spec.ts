import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BusinessCentreComponent } from './business-centre.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { BusinessCentreService, MockBusinessCentreService } from '../../services/business-centre.service';

describe('BusinessCentreComponent', () => {
    let component: BusinessCentreComponent;
    let fixture: ComponentFixture<BusinessCentreComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, RouterTestingModule, ModalModule.forRoot(), FormsModule],
            declarations: [BusinessCentreComponent],
            providers: [{ provide: BusinessCentreService, useClass: MockBusinessCentreService }]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(BusinessCentreComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
