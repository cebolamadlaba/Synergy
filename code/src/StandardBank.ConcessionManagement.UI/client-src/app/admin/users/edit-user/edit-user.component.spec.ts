import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditUserComponent } from './edit-user.component';
import { AdminService, MockAdminService } from "../../../services/admin.service";
import { HttpModule } from '@angular/http';
import { RouterTestingModule } from '@angular/router/testing';

describe('EditUserComponent', () => {
    let component: EditUserComponent;
    let fixture: ComponentFixture<EditUserComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, FormsModule, RouterTestingModule],
            declarations: [EditUserComponent],
            providers: [{ provide: AdminService, useClass: MockAdminService }]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EditUserComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});
