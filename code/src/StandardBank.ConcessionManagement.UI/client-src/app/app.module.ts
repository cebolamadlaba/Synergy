import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PageHeaderComponent } from './page-header/page-header.component';
import { PendingInboxComponent } from './pending-inbox/pending-inbox.component';
import { ApprovedConcessionsComponent } from './approved-concessions/approved-concessions.component';
import { ConditionsComponent } from './conditions/conditions.component';
import { PricingComponent } from './pricing/pricing.component';
import { DueExpiryInboxComponent } from './due-expiry-inbox/due-expiry-inbox.component';
import { ExpiredInboxComponent } from './expired-inbox/expired-inbox.component';
import { DeclinedInboxComponent } from './declined-inbox/declined-inbox.component';
import { InboxHeaderComponent } from './inbox-header/inbox-header.component';
import { InboxSearchBarComponent } from './inbox-search-bar/inbox-search-bar.component';
import { PricingResultsComponent } from './pricing-results/pricing-results.component';
import { PricingLendingComponent } from './pricing-lending/pricing-lending.component';
import { PricingCashComponent } from './pricing-cash/pricing-cash.component';
import { PricingTransactionalComponent } from './pricing-transactional/pricing-transactional.component';
import { CashAddConcessionComponent } from './cash-add-concession/cash-add-concession.component';
import { LendingAddConcessionComponent } from './lending-add-concession/lending-add-concession.component';
import { TransactionalAddConcessionComponent } from './transactional-add-concession/transactional-add-concession.component';

import { ModalModule } from 'ngx-bootstrap/modal';
import { MismatchedInboxComponent } from './mismatched-inbox/mismatched-inbox.component';
import { UserConcessionsService } from "./user-concessions/user-concessions.service";
import { UserService } from "./user/user.service";

@NgModule({
  declarations: [
    AppComponent,
    PageHeaderComponent,
    PendingInboxComponent,
    ApprovedConcessionsComponent,
    ConditionsComponent,
    PricingComponent,
    DueExpiryInboxComponent,
    ExpiredInboxComponent,
    DeclinedInboxComponent,
    InboxHeaderComponent,
    InboxSearchBarComponent,
    PricingResultsComponent,
    PricingLendingComponent,
    PricingCashComponent,
    PricingTransactionalComponent,
    CashAddConcessionComponent,
    LendingAddConcessionComponent,
    TransactionalAddConcessionComponent,
    MismatchedInboxComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    AppRoutingModule,
    ModalModule.forRoot()
  ],
  providers: [UserConcessionsService, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
