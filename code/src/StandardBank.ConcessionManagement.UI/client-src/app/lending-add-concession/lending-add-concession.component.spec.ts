import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LendingAddConcessionComponent } from './lending-add-concession.component';

describe('LendingAddConcessionComponent', () => {
  let component: LendingAddConcessionComponent;
  let fixture: ComponentFixture<LendingAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LendingAddConcessionComponent],
      imports: [ModalModule.forRoot()]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LendingAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
