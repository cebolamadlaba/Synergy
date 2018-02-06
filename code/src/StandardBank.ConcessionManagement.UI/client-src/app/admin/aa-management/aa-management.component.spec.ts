import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AaManagementComponent } from './aa-management.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { AaManagementService, MockAaManagementService } from '../../services/aa-management.service';

describe('AaManagementComponent', () => {
    let component: AaManagementComponent;
    let fixture: ComponentFixture<AaManagementComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, RouterTestingModule, ModalModule.forRoot(), FormsModule],
            declarations: [AaManagementComponent],
            providers: [
                { provide: AaManagementService, useClass: MockAaManagementService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AaManagementComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
