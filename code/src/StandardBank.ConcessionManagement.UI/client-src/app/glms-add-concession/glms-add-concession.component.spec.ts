import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlmsAddConcessionComponent } from './glms-add-concession.component';

describe('GlmsAddConcessionComponent', () => {
  let component: GlmsAddConcessionComponent;
  let fixture: ComponentFixture<GlmsAddConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlmsAddConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlmsAddConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
