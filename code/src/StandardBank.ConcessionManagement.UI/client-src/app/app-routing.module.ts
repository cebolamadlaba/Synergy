import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PendingInboxComponent } from "./pending-inbox/pending-inbox.component";
import { ApprovedConcessionsComponent } from "./approved-concessions/approved-concessions.component";
import { ConditionsComponent } from "./conditions/conditions.component";
import { PricingComponent } from "./pricing/pricing.component";

const routes: Routes = [
  { path: '', redirectTo: '/pending-inbox', pathMatch: 'full' },
  { path: 'pending-inbox', component: PendingInboxComponent },
  { path: 'approved-concessions', component: ApprovedConcessionsComponent },
  { path: 'conditions', component: ConditionsComponent },
  { path: 'pricing', component: PricingComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
