import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { InboxHeaderComponent } from './inbox-header.component';
import { UserConcessionsService, MockUserConcessionsService } from "../services/user-concessions.service";

describe('InboxHeaderComponent', () => {
    let component: InboxHeaderComponent;
    let fixture: ComponentFixture<InboxHeaderComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule],
            declarations: [InboxHeaderComponent],
            providers: [{ provide: UserConcessionsService, useClass: MockUserConcessionsService }]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(InboxHeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(component).toBeTruthy();
    });
});

