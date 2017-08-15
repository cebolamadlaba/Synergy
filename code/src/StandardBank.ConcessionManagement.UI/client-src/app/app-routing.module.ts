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
import { TransactionalAddConcessionComponent } from "./transactional-add-concession/transactional-add-concession.component";
import { CashAddConcessionComponent } from "./cash-add-concession/cash-add-concession.component";
import { LendingAddConcessionComponent } from "./lending-add-concession/lending-add-concession.component";
import { MismatchedInboxComponent } from "./mismatched-inbox/mismatched-inbox.component";
import { LendingEditConcessionComponent } from "./lending-edit-concession/lending-edit-concession.component";
import { LendingViewConcessionComponent } from "./lending-view-concession/lending-view-concession.component";

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
    { path: 'pricing-cash', component: PricingCashComponent },
    { path: 'pricing-transactional', component: PricingTransactionalComponent },
    { path: 'transactional-add-concession', component: TransactionalAddConcessionComponent },
    { path: 'cash-add-concession', component: CashAddConcessionComponent },
    { path: 'lending-add-concession/:riskGroupNumber', component: LendingAddConcessionComponent },
    { path: 'lending-edit-concession/:riskGroupNumber/:concessionReferenceId', component: LendingEditConcessionComponent },
    { path: 'lending-view-concession/:riskGroupNumber/:concessionReferenceId', component: LendingViewConcessionComponent },
    { path: 'lending-inbox-view-concession/:riskGroupNumber/:concessionReferenceId', component: LendingViewConcessionComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
