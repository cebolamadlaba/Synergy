import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { ApprovedConcessionsComponent } from './approved-concessions.component';
import { FormsModule } from '@angular/forms';
import { UserConcessionsService, MockUserConcessionsService } from "../user-concessions/user-concessions.service";
import { ApprovedConcessionFilterPipe } from "../approved-concession-filter/approved-concession-filter.pipe";
import { RouterTestingModule } from '@angular/router/testing';

describe('ApprovedConcessionsComponent', () => {
    let component: ApprovedConcessionsComponent;
    let fixture: ComponentFixture<ApprovedConcessionsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, FormsModule, RouterTestingModule],
            declarations: [ApprovedConcessionsComponent, ApprovedConcessionFilterPipe],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ApprovedConcessionsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});