import { TestBed, async } from "@angular/core/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { HttpModule } from '@angular/http';
import { AppComponent } from "./app.component";
import { PageHeaderComponent } from './page-header/page-header.component';
import { UserService, MockUserService } from "./services/user.service";

describe("AppComponent", () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpModule],
            declarations: [AppComponent, PageHeaderComponent],
            providers: [{ provide: UserService, useClass: MockUserService }]
        }).compileComponents();
    }));

    it("should create the app", async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));
});
