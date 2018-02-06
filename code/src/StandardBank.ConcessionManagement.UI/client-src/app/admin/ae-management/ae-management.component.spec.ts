import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AeManagementComponent } from './ae-management.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { AeManagementService, MockAeManagementService } from '../../services/ae-management.service';
import { UserService, MockUserService } from '../../services/user.service';

describe('AeManagementComponent', () => {
    let component: AeManagementComponent;
    let fixture: ComponentFixture<AeManagementComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, RouterTestingModule, ModalModule.forRoot(), FormsModule],
            declarations: [AeManagementComponent],
            providers: [
                { provide: AeManagementService, useClass: MockAeManagementService },
                { provide: UserService, useClass: MockUserService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AeManagementComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
