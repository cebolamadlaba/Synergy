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

const routes: Routes = [
    { path: '', redirectTo: '/pending-inbox', pathMatch: 'full' },
    { path: 'pending-inbox', component: PendingInboxComponent },
    { path: 'due-expiry-inbox', component: DueExpiryInboxComponent },
    { path: 'expired-inbox', component: ExpiredInboxComponent },
    { path: 'mismatched-inbox', component: MismatchedInboxComponent },
    { path: 'declined-inbox', component: DeclinedInboxComponent },
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
    { path: 'cash-add-concession/:riskGroupNumber', component: CashAddConcessionComponent },
    { path: 'cash-view-concession/:riskGroupNumber/:concessionReferenceId', component: CashViewConcessionComponent },
    { path: 'lending-add-concession/:riskGroupNumber', component: LendingAddConcessionComponent },
    { path: 'lending-view-concession/:riskGroupNumber/:concessionReferenceId', component: LendingViewConcessionComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
