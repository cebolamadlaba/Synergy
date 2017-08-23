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
import { LendingEditConcessionComponent } from './lending-edit-concession/lending-edit-concession.component';
import { LendingViewConcessionComponent } from './lending-view-concession/lending-view-concession.component';

import { UserConcessionsService } from "./services/user-concessions.service";
import { UserService } from "./services/user.service";
import { RiskGroupService } from "./services/risk-group.service";
import { LendingViewService } from "./services/lending-view.service";
import { ReviewFeeTypeService } from "./services/review-fee-type.service";
import { ProductTypeService } from "./services/product-type.service";
import { PeriodService } from "./services/period.service";
import { PeriodTypeService } from "./services/period-type.service";
import { ConditionTypeService } from "./services/condition-type.service";
import { ConcessionConditionsService } from "./services/concession-conditions.service";
import { ClientAccountService } from "./services/client-account.service";
import { LendingNewService } from "./services/lending-new.service";
import { LendingService } from "./services/lending.service";
import { LendingUpdateService } from "./services/lending-update.service";
import { MyConditionService } from './services/my-condition.service';
import { CashConcessionService } from "./services/cash-concession.service";
import { AccrualTypeService } from "./services/accrual-type.service";
import { ChannelTypeService } from "./services/channel-type.service";

import { LendingConcessionFilterPipe } from './filters/lending-concession-filter.pipe';
import { CashConcessionFilterPipe } from './filters/cash-concession-filter.pipe';
import { ApprovedConcessionFilterPipe } from './filters/approved-concession-filter.pipe';


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
        LendingConcessionFilterPipe,
        LendingEditConcessionComponent,
        LendingViewConcessionComponent,
        ApprovedConcessionFilterPipe,
        CashConcessionFilterPipe
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
    providers: [
        UserConcessionsService,
        UserService,
        RiskGroupService,
        LendingViewService,
        ReviewFeeTypeService,
        ProductTypeService,
        PeriodService,
        PeriodTypeService,
        ConditionTypeService,
        ConcessionConditionsService,
        ClientAccountService,
        LendingNewService,
        LendingService,
        LendingUpdateService,
        MyConditionService,
        CashConcessionService,
        AccrualTypeService,
        ChannelTypeService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
