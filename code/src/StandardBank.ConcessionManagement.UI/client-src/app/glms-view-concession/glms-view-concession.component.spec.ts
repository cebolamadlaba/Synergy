import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlmsViewConcessionComponent } from './glms-view-concession.component';

describe('GlmsViewConcessionComponent', () => {
  let component: GlmsViewConcessionComponent;
  let fixture: ComponentFixture<GlmsViewConcessionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlmsViewConcessionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlmsViewConcessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
