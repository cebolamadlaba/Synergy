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
import { ReviewFeeTypeService } from "./review-fee-type/review-fee-type.service";
import { ProductTypeService } from "./product-type/product-type.service";
import { PeriodService } from "./period/period.service";
import { PeriodTypeService } from "./period-type/period-type.service";
import { ConditionTypeService } from "./condition-type/condition-type.service";
import { ConcessionConditionsService } from "./concession-conditions/concession-conditions.service";
import { ClientAccountService } from "./client-account/client-account.service";
import { LendingNewService } from "./lending-new/lending-new.service";
import { LendingEditConcessionComponent } from './lending-edit-concession/lending-edit-concession.component';
import { LendingService } from "./lending/lending.service";
import { LendingUpdateService } from "./lending-update/lending-update.service";
import { MyConditionService } from './my-condition/my-condition.service';
import { LendingViewConcessionComponent } from './lending-view-concession/lending-view-concession.component';
import { ApprovedConcessionFilterPipe } from './approved-concession-filter/approved-concession-filter.pipe';
import { CashConcessionService } from "./cash-concession/cash-concession.service";
import { CashConcessionFilterPipe } from './cash-concession-filter/cash-concession-filter.pipe';
import { AccrualTypeService } from "./accrual-type/accrual-type.service";
import { ChannelTypeService } from "./channel-type/channel-type.service";

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
