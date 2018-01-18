import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PendingInboxComponent } from "./pending-inbox/pending-inbox.component";
import { ApprovedConcessionsComponent } from "./approved-concessions/approved-concessions.component";
import { ConditionsComponent } from "./conditions/conditions.component";
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
import { InvestmentsAddConcessionComponent } from "./investments-add-concession/investments-add-concession.component";
import { BolAddConcessionComponent } from "./bol-add-concession/bol-add-concession.component";
import { MasAddConcessionComponent } from "./mas-add-concession/mas-add-concession.component";
import { TransactionalViewConcessionComponent } from "./transactional-view-concession/transactional-view-concession.component";
import { TradeAddConcessionComponent } from "./trade-add-concession/trade-add-concession.component";
import { CashmanAddConcessionComponent } from "./cashman-add-concession/cashman-add-concession.component";
import { AdminMenuComponent } from "./admin/admin-menu/admin-menu.component";
import { BusinessCentreComponent } from "./admin/business-centre/business-centre.component";
import { UsersComponent } from "./admin/users/users.component";
import { EditUserComponent } from './admin/users/edit-user/edit-user.component';
import { ActionedInboxComponent } from "./actioned-inbox/actioned-inbox.component";
import { RegionComponent } from './admin/region/region.component';

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
    { path: 'pricing', component: PricingComponent },
    { path: 'pricing/:riskGroupNumber', component: PricingComponent },
    { path: 'pricing-lending/:riskGroupNumber', component: PricingLendingComponent },
    { path: 'pricing-cash/:riskGroupNumber', component: PricingCashComponent },
    { path: 'pricing-transactional/:riskGroupNumber', component: PricingTransactionalComponent },
    { path: 'pricing-investments', component: PricingInvestmentsComponent },
    { path: 'pricing-bol', component: PricingBolComponent },
    { path: 'pricing-trade', component: PricingTradeComponent },
    { path: 'pricing-mas', component: PricingMasComponent },
    { path: 'pricing-cashman', component: PricingCashmanComponent },
    { path: 'transactional-add-concession/:riskGroupNumber', component: TransactionalAddConcessionComponent },
    { path: 'transactional-view-concession/:riskGroupNumber/:concessionReferenceId', component: TransactionalViewConcessionComponent },
    { path: 'cash-add-concession/:riskGroupNumber', component: CashAddConcessionComponent },
    { path: 'cash-view-concession/:riskGroupNumber/:concessionReferenceId', component: CashViewConcessionComponent },
    { path: 'lending-add-concession/:riskGroupNumber', component: LendingAddConcessionComponent },
    { path: 'lending-view-concession/:riskGroupNumber/:concessionReferenceId', component: LendingViewConcessionComponent },
    { path: 'investments-add-concession', component: InvestmentsAddConcessionComponent },
    { path: 'bol-add-concession', component: BolAddConcessionComponent },
    { path: 'mas-add-concession', component: MasAddConcessionComponent },
    { path: 'lending-view-concession/:riskGroupNumber/:concessionReferenceId', component: LendingViewConcessionComponent },
    { path: 'trade-add-concession', component: TradeAddConcessionComponent },
    { path: 'cashman-add-concession', component: CashmanAddConcessionComponent },
    { path: 'lending-view-concession/:riskGroupNumber/:concessionReferenceId', component: LendingViewConcessionComponent },
    { path: 'admin-menu', component: AdminMenuComponent },
    { path: 'business-centre', component: BusinessCentreComponent },
    { path: 'users', component: UsersComponent },
    { path: 'edit-user/:id', component: EditUserComponent },
    { path: 'admin/regions', component: RegionComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
