import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BcmManagementComponent } from './bcm-management.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { BcmManagementService, MockBcmManagementService } from '../../services/bcm-management.service';
import { AdminService, MockAdminService } from '../../services/admin.service';

describe('BcmManagementComponent', () => {
    let component: BcmManagementComponent;
    let fixture: ComponentFixture<BcmManagementComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, RouterTestingModule, ModalModule.forRoot(), FormsModule],
            declarations: [BcmManagementComponent],
            providers: [
                { provide: BcmManagementService, useClass: MockBcmManagementService },
                { provide: AdminService, useClass: MockAdminService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(BcmManagementComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
