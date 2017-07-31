import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DataTablesModule } from 'angular-datatables';

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
import { PricingLendingComponent } from './pricing-lending/pricing-lending.component';
import { PricingCashComponent } from './pricing-cash/pricing-cash.component';
import { PricingTransactionalComponent } from './pricing-transactional/pricing-transactional.component';
import { CashAddConcessionComponent } from './cash-add-concession/cash-add-concession.component';
import { LendingAddConcessionComponent } from './lending-add-concession/lending-add-concession.component';
import { TransactionalAddConcessionComponent } from './transactional-add-concession/transactional-add-concession.component';
import { MismatchedInboxComponent } from './mismatched-inbox/mismatched-inbox.component';

import { UserConcessionsService } from "./user-concessions/user-concessions.service";
import { UserService } from "./user/user.service";
import { RiskGroupService } from "./risk-group/risk-group.service";
import { LendingViewService } from "./lending-view/lending-view.service";
import { LendingConcessionFilterPipe } from './lending-concession-filter/lending-concession-filter.pipe';

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
        PricingLendingComponent,
        PricingCashComponent,
        PricingTransactionalComponent,
        CashAddConcessionComponent,
        LendingAddConcessionComponent,
        TransactionalAddConcessionComponent,
        MismatchedInboxComponent,
        LendingConcessionFilterPipe
    ],
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        AppRoutingModule,
        ModalModule.forRoot(),
        DataTablesModule,
        ReactiveFormsModule
    ],
    providers: [UserConcessionsService, UserService, RiskGroupService, LendingViewService],
    bootstrap: [AppComponent]
})
export class AppModule { }
