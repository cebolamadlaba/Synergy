import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminMenuComponent } from './admin-menu.component';
import { HttpModule } from '@angular/http';
import { UserService, MockUserService } from '../../services/user.service';

describe('AdminMenuComponent', () => {
    let component: AdminMenuComponent;
    let fixture: ComponentFixture<AdminMenuComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            declarations: [AdminMenuComponent],
            providers: [{ provide: UserService, useClass: MockUserService }]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AdminMenuComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
