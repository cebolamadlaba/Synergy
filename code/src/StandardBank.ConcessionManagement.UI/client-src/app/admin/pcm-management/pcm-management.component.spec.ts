import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PcmManagementComponent } from './pcm-management.component';
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { PcmManagementService, MockPcmManagementService } from '../../services/pcm-management.service';
import { UserService, MockUserService } from '../../services/user.service';

describe('PcmManagementComponent', () => {
    let component: PcmManagementComponent;
    let fixture: ComponentFixture<PcmManagementComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, RouterTestingModule, ModalModule.forRoot(), FormsModule],
            declarations: [PcmManagementComponent],
            providers: [
                { provide: PcmManagementService, useClass: MockPcmManagementService },
                { provide: UserService, useClass: MockUserService }
            ]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PcmManagementComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
