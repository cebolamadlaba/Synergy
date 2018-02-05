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
import { LendingViewConcessionComponent } from './lending-view-concession/lending-view-concession.component';

import { LookupDataService } from "./services/lookup-data.service";
import { UserConcessionsService } from "./services/user-concessions.service";
import { UserService } from "./services/user.service";
import { ConcessionConditionsService } from "./services/concession-conditions.service";
import { LendingService } from "./services/lending.service";
import { MyConditionService } from './services/my-condition.service';
import { CashConcessionService } from "./services/cash-concession.service";
import { AdminService } from './services/admin.service';
import { RegionService } from './services/region.service';
import { BusinessCentreService } from './services/business-centre.service';
import { PcmManagementService } from './services/pcm-management.service';
import { BcmManagementService } from './services/bcm-management.service';

import { ApprovedConcessionFilterPipe } from './filters/approved-concession-filter.pipe';
import { CashViewConcessionComponent } from './cash-view-concession/cash-view-concession.component';
import { PricingInvestmentsComponent } from './pricing-investments/pricing-investments.component';
import { PricingBolComponent } from './pricing-bol/pricing-bol.component';
import { PricingTradeComponent } from './pricing-trade/pricing-trade.component';
import { PricingMasComponent } from './pricing-mas/pricing-mas.component';
import { PricingCashmanComponent } from './pricing-cashman/pricing-cashman.component';
import { TransactionalConcessionService } from "./services/transactional-concession.service";
import { BaseConcessionFilterPipe } from './filters/base-concession-filter.pipe';
import { InvestmentsAddConcessionComponent } from './investments-add-concession/investments-add-concession.component';
import { BolAddConcessionComponent } from './bol-add-concession/bol-add-concession.component';
import { MasAddConcessionComponent } from './mas-add-concession/mas-add-concession.component';
import { TransactionalViewConcessionComponent } from './transactional-view-concession/transactional-view-concession.component';
import { TradeAddConcessionComponent } from './trade-add-concession/trade-add-concession.component';
import { CashmanAddConcessionComponent } from './cashman-add-concession/cashman-add-concession.component';
import { AdminMenuComponent } from './admin/admin-menu/admin-menu.component';
import { BusinessCentreComponent } from './admin/business-centre/business-centre.component';
import { UsersComponent } from './admin/users/users.component';
import { EditUserComponent } from './admin/users/edit-user/edit-user.component';
import { ActionedInboxComponent } from './actioned-inbox/actioned-inbox.component';
import { ConditionsFilterPipe } from './filters/conditions-filter.pipe';
import { NumbersonlyDirective } from './directives/numbersonly.directive';
import { RegionComponent } from './admin/region/region.component';
import { PcmManagementComponent } from './admin/pcm-management/pcm-management.component';
import { BcmManagementComponent } from './admin/bcm-management/bcm-management.component';

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
        LendingViewConcessionComponent,
        ApprovedConcessionFilterPipe,
        CashViewConcessionComponent,
        BaseConcessionFilterPipe,
        CashViewConcessionComponent,
        PricingInvestmentsComponent,
        PricingBolComponent,
        PricingTradeComponent,
        PricingMasComponent,
        PricingCashmanComponent,
        InvestmentsAddConcessionComponent,
        BolAddConcessionComponent,
        MasAddConcessionComponent,
        TransactionalViewConcessionComponent,
        TradeAddConcessionComponent,
        CashmanAddConcessionComponent,
        AdminMenuComponent,
        BusinessCentreComponent,
        UsersComponent,
        EditUserComponent,
        NumbersonlyDirective,
        ActionedInboxComponent,
        ConditionsFilterPipe,
        RegionComponent,
        PcmManagementComponent,
        BcmManagementComponent
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
        LookupDataService,
        UserConcessionsService,
        UserService,
        ConcessionConditionsService,
        LendingService,
        MyConditionService,
        CashConcessionService,
        TransactionalConcessionService,
        AdminService,
        RegionService,
        BusinessCentreService,
        PcmManagementService,
        BcmManagementService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
