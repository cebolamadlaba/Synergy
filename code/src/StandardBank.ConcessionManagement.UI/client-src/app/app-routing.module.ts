import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PendingInboxComponent } from "./pending-inbox/pending-inbox.component";
import { ApprovedConcessionsComponent } from "./approved-concessions/approved-concessions.component";
import { ConditionsComponent } from "./conditions/conditions.component";
import { SearchComponent } from "./search/search.component";
import { PricingComponent } from "./pricing/pricing.component";
import { DueExpiryInboxComponent } from "./due-expiry-inbox/due-expiry-inbox.component";
import { ExpiredInboxComponent } from "./expired-inbox/expired-inbox.component";
import { DeclinedInboxComponent } from "./declined-inbox/declined-inbox.component";
import { PricingLendingComponent } from "./pricing-lending/pricing-lending.component";
import { PricingCashComponent } from "./pricing-cash/pricing-cash.component";
import { PricingTransactionalComponent } from "./pricing-transactional/pricing-transactional.component";
import { PricingInvestmentsComponent } from "./pricing-investments/pricing-investments.component";
import { PricingBolComponent } from "./pricing-bol/pricing-bol.component";
import { PricingTradeComponent } from "./pricing-trade/pricing-trade.component";
import { PricingMasComponent } from "./pricing-mas/pricing-mas.component";
import { PricingCashmanComponent } from "./pricing-cashman/pricing-cashman.component";
import { TransactionalAddConcessionComponent } from "./transactional-add-concession/transactional-add-concession.component";
import { CashAddConcessionComponent } from "./cash-add-concession/cash-add-concession.component";
import { LendingAddConcessionComponent } from "./lending-add-concession/lending-add-concession.component";
import { MismatchedInboxComponent } from "./mismatched-inbox/mismatched-inbox.component";
import { LendingViewConcessionComponent } from "./lending-view-concession/lending-view-concession.component";
import { CashViewConcessionComponent } from "./cash-view-concession/cash-view-concession.component";

import { InvestmentAddConcessionComponent } from "./investments-add-concession/investments-add-concession.component";
import { InvestmentsViewConcessionComponent } from "./investments-view-concession/investments-view-concession.component";

import { BolAddConcessionComponent } from "./bol-add-concession/bol-add-concession.component";
import { BolViewConcessionComponent } from "./bol-view-concession/bol-view-concession.component";

import { MasAddConcessionComponent } from "./mas-add-concession/mas-add-concession.component";
import { TransactionalViewConcessionComponent } from "./transactional-view-concession/transactional-view-concession.component";
import { TradeAddConcessionComponent } from "./trade-add-concession/trade-add-concession.component";
import { TradeViewConcessionComponent } from "./trade-view-concession/trade-view-concession.component";

import { CashmanAddConcessionComponent } from "./cashman-add-concession/cashman-add-concession.component";
import { AdminMenuComponent } from "./admin/admin-menu/admin-menu.component";
import { BusinessCentreComponent } from "./admin/business-centre/business-centre.component";
import { UsersComponent } from "./admin/users/users.component";
import { EditUserComponent } from './admin/users/edit-user/edit-user.component';
import { ActionedInboxComponent } from "./actioned-inbox/actioned-inbox.component";
import { RegionComponent } from './admin/region/region.component';
import { PcmManagementComponent } from './admin/pcm-management/pcm-management.component';
import { BcmManagementComponent } from './admin/bcm-management/bcm-management.component';
import { AeManagementComponent } from './admin/ae-management/ae-management.component';
import { AaManagementComponent } from './admin/aa-management/aa-management.component';
import { BolTradeManagementComponent } from './admin/bol-trade-management/bol-trade-management.component';
import { BolTradeAeManagementComponent } from './admin/bol-trade-ae-management/bol-trade-ae-management.component';
import { ApprovedConcessionLetterViewComponent } from './admin/approved-concession-letter-view/approved-concession-letter-view.component';

import { BOLCHManagementComponent } from './admin/bol-chargecodes/bol-chargecodes.component';
import { TransactionTypesManagementComponent } from './admin/transaction-types/transaction-types.component';
import { ChannelTypesManagementComponent } from './admin/channel-types/channel-types.component';
import { PricingGlmsComponent } from './pricing-glms/pricing-glms.component';
import { GlmsAddConcessionComponent } from './glms-add-concession/glms-add-concession.component';
import { GlmsViewConcessionComponent } from './glms-view-concession/glms-view-concession.component';




const routes: Routes = [
    { path: '', redirectTo: '/pending-inbox', pathMatch: 'full' },
    { path: 'pending-inbox', component: PendingInboxComponent },
    { path: 'due-expiry-inbox', component: DueExpiryInboxComponent },
    { path: 'expired-inbox', component: ExpiredInboxComponent },
    { path: 'mismatched-inbox', component: MismatchedInboxComponent },
    { path: 'declined-inbox', component: DeclinedInboxComponent },
    { path: 'actioned-inbox', component: ActionedInboxComponent },
    { path: 'approved-concessions', component: ApprovedConcessionsComponent },
    { path: 'conditions', component: ConditionsComponent },
    { path: 'search', component: SearchComponent },
    { path: 'pricing', component: PricingComponent },
    { path: 'pricing/:riskGroupNumber', component: PricingComponent },
    { path: 'pricing/:riskGroupNumber/:sapbpid', component: PricingComponent },
    { path: 'pricing-lending/:riskGroupNumber/:sapbpid', component: PricingLendingComponent },
    { path: 'pricing-cash/:riskGroupNumber/:sapbpid', component: PricingCashComponent },
    { path: 'pricing-transactional/:riskGroupNumber/:sapbpid', component: PricingTransactionalComponent },
    { path: 'pricing-investments/:riskGroupNumber/:sapbpid', component: PricingInvestmentsComponent },
    { path: 'pricing-bol/:riskGroupNumber/:sapbpid', component: PricingBolComponent },
    { path: 'pricing-trade/:riskGroupNumber/:sapbpid', component: PricingTradeComponent },
    { path: 'pricing-glms/:riskGroupNumber/:sapbpid', component: PricingGlmsComponent },
    { path: 'pricing-mas', component: PricingMasComponent },
    { path: 'pricing-cashman', component: PricingCashmanComponent },

    { path: 'transactional-add-concession/:riskGroupNumber/:sapbpid', component: TransactionalAddConcessionComponent },
    { path: 'transactional-view-concession/:riskGroupNumber/:concessionReferenceId', component: TransactionalViewConcessionComponent },
    { path: 'transactional-view-concession/:riskGroupNumber/:sapbpid/:concessionReferenceId', component: TransactionalViewConcessionComponent },

    { path: 'cash-add-concession/:riskGroupNumber/:sapbpid', component: CashAddConcessionComponent },
    { path: 'cash-view-concession/:riskGroupNumber/:concessionReferenceId', component: CashViewConcessionComponent },
    { path: 'cash-view-concession/:riskGroupNumber/:sapbpid/:concessionReferenceId', component: CashViewConcessionComponent },

    { path: 'lending-add-concession/:riskGroupNumber/:sapbpid', component: LendingAddConcessionComponent },
    { path: 'lending-view-concession/:riskGroupNumber/:concessionReferenceId', component: LendingViewConcessionComponent },
    { path: 'lending-view-concession/:riskGroupNumber/:sapbpid/:concessionReferenceId', component: LendingViewConcessionComponent },

    { path: 'investments-add-concession/:riskGroupNumber/:sapbpid', component: InvestmentAddConcessionComponent },
    { path: 'investments-view-concession/:riskGroupNumber/:concessionReferenceId', component: InvestmentsViewConcessionComponent },
    { path: 'investments-view-concession/:riskGroupNumber/:sapbpid/:concessionReferenceId', component: InvestmentsViewConcessionComponent },

    { path: 'bol-add-concession/:riskGroupNumber/:sapbpid', component: BolAddConcessionComponent },
    { path: 'bol-view-concession/:riskGroupNumber/:concessionReferenceId', component: BolViewConcessionComponent },
    { path: 'bol-view-concession/:riskGroupNumber/:sapbpid/:concessionReferenceId', component: BolViewConcessionComponent },

    { path: 'mas-add-concession', component: MasAddConcessionComponent },

    { path: 'trade-add-concession/:riskGroupNumber/:sapbpid', component: TradeAddConcessionComponent },
    { path: 'trade-view-concession/:riskGroupNumber/:concessionReferenceId', component: TradeViewConcessionComponent },
    { path: 'trade-view-concession/:riskGroupNumber/:sapbpid/:concessionReferenceId', component: TradeViewConcessionComponent },

    { path: 'glms-add-concession/:riskGroupNumber/:sapbpid', component: GlmsAddConcessionComponent },
    { path: 'glms-view-concession/:riskGroupNumber/:concessionReferenceId', component: GlmsViewConcessionComponent },
    { path: 'glms-view-concession/:riskGroupNumber/:sapbpid/:concessionReferenceId', component: GlmsViewConcessionComponent },


    { path: 'cashman-add-concession', component: CashmanAddConcessionComponent },
    { path: 'admin', component: AdminMenuComponent },
    { path: 'admin/business-centre', component: BusinessCentreComponent },
    { path: 'admin/users', component: UsersComponent },
    { path: 'admin/users/edit-user/:id', component: EditUserComponent },
    { path: 'admin/regions', component: RegionComponent },
    { path: 'admin/pcm-management', component: PcmManagementComponent },
    { path: 'admin/bcm-management', component: BcmManagementComponent },
    { path: 'admin/ae-management', component: AeManagementComponent },
    { path: 'admin/aa-management', component: AaManagementComponent },
    { path: 'admin/bol-trade-management', component: BolTradeManagementComponent },
    { path: 'admin/bol-trade-ae-management', component: BolTradeAeManagementComponent },
    { path: 'admin/approved-concession-letter-view', component: ApprovedConcessionLetterViewComponent },

    { path: 'admin/bol-chargecodes', component: BOLCHManagementComponent },
    { path: 'admin/transaction-types', component: TransactionTypesManagementComponent },
    { path: 'admin/channel-types', component: ChannelTypesManagementComponent }


];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
