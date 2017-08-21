import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TransactionalAddConcessionComponent } from './transactional-add-concession.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('TransactionalAddConcessionComponent', () => {
  let component: TransactionalAddConcessionComponent;
  let fixture: ComponentFixture<TransactionalAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [TransactionalAddConcessionComponent],
      imports: [ModalModule.forRoot(), RouterTestingModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionalAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
