webpackJsonp([1],{

/***/ "../../../../../client-src async recursive":
/***/ (function(module, exports) {

function webpackEmptyContext(req) {
	throw new Error("Cannot find module '" + req + "'.");
}
webpackEmptyContext.keys = function() { return []; };
webpackEmptyContext.resolve = webpackEmptyContext;
module.exports = webpackEmptyContext;
webpackEmptyContext.id = "../../../../../client-src async recursive";

/***/ }),

/***/ "../../../../../client-src/app/app-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__pending_inbox_pending_inbox_component__ = __webpack_require__("../../../../../client-src/app/pending-inbox/pending-inbox.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__approved_concessions_approved_concessions_component__ = __webpack_require__("../../../../../client-src/app/approved-concessions/approved-concessions.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__conditions_conditions_component__ = __webpack_require__("../../../../../client-src/app/conditions/conditions.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__pricing_pricing_component__ = __webpack_require__("../../../../../client-src/app/pricing/pricing.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__due_expiry_inbox_due_expiry_inbox_component__ = __webpack_require__("../../../../../client-src/app/due-expiry-inbox/due-expiry-inbox.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__expired_inbox_expired_inbox_component__ = __webpack_require__("../../../../../client-src/app/expired-inbox/expired-inbox.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__declined_inbox_declined_inbox_component__ = __webpack_require__("../../../../../client-src/app/declined-inbox/declined-inbox.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var routes = [
    { path: '', redirectTo: '/pending-inbox', pathMatch: 'full' },
    { path: 'pending-inbox', component: __WEBPACK_IMPORTED_MODULE_2__pending_inbox_pending_inbox_component__["a" /* PendingInboxComponent */] },
    { path: 'due-expiry-inbox', component: __WEBPACK_IMPORTED_MODULE_6__due_expiry_inbox_due_expiry_inbox_component__["a" /* DueExpiryInboxComponent */] },
    { path: 'expired-inbox', component: __WEBPACK_IMPORTED_MODULE_7__expired_inbox_expired_inbox_component__["a" /* ExpiredInboxComponent */] },
    { path: 'declined-inbox', component: __WEBPACK_IMPORTED_MODULE_8__declined_inbox_declined_inbox_component__["a" /* DeclinedInboxComponent */] },
    { path: 'approved-concessions', component: __WEBPACK_IMPORTED_MODULE_3__approved_concessions_approved_concessions_component__["a" /* ApprovedConcessionsComponent */] },
    { path: 'conditions', component: __WEBPACK_IMPORTED_MODULE_4__conditions_conditions_component__["a" /* ConditionsComponent */] },
    { path: 'pricing', component: __WEBPACK_IMPORTED_MODULE_5__pricing_pricing_component__["a" /* PricingComponent */] }
];
var AppRoutingModule = (function () {
    function AppRoutingModule() {
    }
    return AppRoutingModule;
}());
AppRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forRoot(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */]]
    })
], AppRoutingModule);

//# sourceMappingURL=app-routing.module.js.map

/***/ }),

/***/ "../../../../../client-src/app/app.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "<app-page-header></app-page-header>\n\n<router-outlet></router-outlet>\n\n<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>\n<script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js\"></script>"

/***/ }),

/***/ "../../../../../client-src/app/app.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var AppComponent = (function () {
    function AppComponent() {
    }
    return AppComponent;
}());
AppComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-root',
        template: __webpack_require__("../../../../../client-src/app/app.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/app.component.css")]
    })
], AppComponent);

//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/app.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__ = __webpack_require__("../../../platform-browser/@angular/platform-browser.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_routing_module__ = __webpack_require__("../../../../../client-src/app/app-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app_component__ = __webpack_require__("../../../../../client-src/app/app.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__page_header_page_header_component__ = __webpack_require__("../../../../../client-src/app/page-header/page-header.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__pending_inbox_pending_inbox_component__ = __webpack_require__("../../../../../client-src/app/pending-inbox/pending-inbox.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__approved_concessions_approved_concessions_component__ = __webpack_require__("../../../../../client-src/app/approved-concessions/approved-concessions.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__conditions_conditions_component__ = __webpack_require__("../../../../../client-src/app/conditions/conditions.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__pricing_pricing_component__ = __webpack_require__("../../../../../client-src/app/pricing/pricing.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__due_expiry_inbox_due_expiry_inbox_component__ = __webpack_require__("../../../../../client-src/app/due-expiry-inbox/due-expiry-inbox.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__expired_inbox_expired_inbox_component__ = __webpack_require__("../../../../../client-src/app/expired-inbox/expired-inbox.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__declined_inbox_declined_inbox_component__ = __webpack_require__("../../../../../client-src/app/declined-inbox/declined-inbox.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__inbox_header_inbox_header_component__ = __webpack_require__("../../../../../client-src/app/inbox-header/inbox-header.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__inbox_search_bar_inbox_search_bar_component__ = __webpack_require__("../../../../../client-src/app/inbox-search-bar/inbox-search-bar.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};














var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["b" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_3__app_component__["a" /* AppComponent */],
            __WEBPACK_IMPORTED_MODULE_4__page_header_page_header_component__["a" /* PageHeaderComponent */],
            __WEBPACK_IMPORTED_MODULE_5__pending_inbox_pending_inbox_component__["a" /* PendingInboxComponent */],
            __WEBPACK_IMPORTED_MODULE_6__approved_concessions_approved_concessions_component__["a" /* ApprovedConcessionsComponent */],
            __WEBPACK_IMPORTED_MODULE_7__conditions_conditions_component__["a" /* ConditionsComponent */],
            __WEBPACK_IMPORTED_MODULE_8__pricing_pricing_component__["a" /* PricingComponent */],
            __WEBPACK_IMPORTED_MODULE_9__due_expiry_inbox_due_expiry_inbox_component__["a" /* DueExpiryInboxComponent */],
            __WEBPACK_IMPORTED_MODULE_10__expired_inbox_expired_inbox_component__["a" /* ExpiredInboxComponent */],
            __WEBPACK_IMPORTED_MODULE_11__declined_inbox_declined_inbox_component__["a" /* DeclinedInboxComponent */],
            __WEBPACK_IMPORTED_MODULE_12__inbox_header_inbox_header_component__["a" /* InboxHeaderComponent */],
            __WEBPACK_IMPORTED_MODULE_13__inbox_search_bar_inbox_search_bar_component__["a" /* InboxSearchBarComponent */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_2__app_routing_module__["a" /* AppRoutingModule */]
        ],
        providers: [],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_3__app_component__["a" /* AppComponent */]]
    })
], AppModule);

//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ "../../../../../client-src/app/approved-concessions/approved-concessions.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/approved-concessions/approved-concessions.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"col-md-12 search-and-results-container\">\r\n  <!-- Search bar -->\r\n  <div class=\"input-group add-on\">\r\n    <input class=\"form-control\" placeholder=\"Search Concession ID or Risk Group Number\" name=\"srch-term\" id=\"srch-term\" type=\"text\">\r\n  </div>\r\n  <!-- search results -->\r\n\r\n  <div class=\"section table-hover-highlight\">\r\n    <div class=\"col-md-12 section-header\">\r\n      <div class=\"concessionID-section\">\r\n        <div class=\"col-md-2 no-padding-left\">\r\n          <div class=\"col-md-1 no-padding-left vertical-align-center\">\r\n            <input type=\"checkbox\">\r\n          </div>\r\n          <div class=\"col-md-10 no-padding-left\">\r\n            <div class=\"concession-name\">EDCON</div>\r\n            <div>1989</div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-md-10\">\r\n          <div class=\"single-print\"><i class=\"fa fa-print\" aria-hidden=\"true\"></i></div>\r\n        </div>\r\n        <div class=\"col-md-10 no-padding-left\">\r\n          <div class=\"col-md-2 no-padding-left\">\r\n            Concession ID :ED0023\r\n          </div>\r\n          <div class=\"col-md-2\">\r\n            Segment:Expert\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <div class=\"section-body\">\r\n      <div class=\"table-container\">\r\n        <table class=\"table table-bordered table-hover header-fixed table-striped\">\r\n          <thead>\r\n            <tr>\r\n              <th>Customer</th>\r\n              <th>Type</th>\r\n              <th>Status</th>\r\n              <th>Date Opened</th>\r\n              <th>Date Sent For Approval</th>\r\n            </tr>\r\n          </thead>\r\n          <tbody>\r\n            <tr>\r\n              <td> CNA </td>\r\n              <td>Lending</td>\r\n              <td>Approved</td>\r\n              <td>2017/05/02 </td>\r\n              <td> 2017/05/03 </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n      </div>\r\n    </div>\r\n  </div>\r\n\r\n  <div class=\"section table-hover-highlight\">\r\n    <div class=\"col-md-12 section-header\">\r\n      <div class=\"concessionID-section\">\r\n        <div class=\"col-md-2 no-padding-left\">\r\n          <div class=\"col-md-1 no-padding-left vertical-align-center\">\r\n            <input type=\"checkbox\">\r\n          </div>\r\n          <div class=\"col-md-10 no-padding-left\">\r\n            <div class=\"concession-name\">APPLE</div>\r\n            <div>1989</div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-md-10\">\r\n          <div class=\"single-print\"><i class=\"fa fa-print\" aria-hidden=\"true\"></i></div>\r\n        </div>\r\n        <div class=\"col-md-10 no-padding-left\">\r\n          <div class=\"col-md-2 no-padding-left\">\r\n            Concession ID :AP0023\r\n          </div>\r\n          <div class=\"col-md-2\">\r\n            Segment:Expert\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <div class=\"section-body\">\r\n      <div class=\"table-container\">\r\n        <table class=\"table table-bordered table-hover header-fixed table-striped\">\r\n          <thead>\r\n            <tr>\r\n              <th>Customer</th>\r\n              <th>Type</th>\r\n              <th>Status</th>\r\n              <th>Date Opened</th>\r\n              <th>Date Sent For Approval</th>\r\n            </tr>\r\n          </thead>\r\n          <tbody>\r\n            <tr>\r\n              <td> CNA </td>\r\n              <td>Lending</td>\r\n              <td>Approved</td>\r\n              <td>2017/05/02 </td>\r\n              <td> 2017/05/03 </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n      </div>\r\n    </div>\r\n  </div>\r\n\r\n  <!-- footer-->\r\n  <div class=\"concession-footer\">\r\n    <!--print button-->\r\n    <button type=\"button\" class=\"btn btn-print\">\r\n      <i class=\"fa fa-print\" aria-hidden=\"true\"></i> Print 1 Files\r\n    </button>\r\n    <!-- pagination-->\r\n    <ul class=\"pagination\">\r\n      <li><a href=\"#\">First</a></li>\r\n      <li><a href=\"#\">Prev</a></li>\r\n      <li class=\"active\"><a href=\"#\">1</a></li>\r\n      <li><a href=\"#\">2</a></li>\r\n      <li><a href=\"#\">Next</a></li>\r\n      <li><a href=\"#\">Last</a></li>\r\n    </ul>\r\n  </div>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/approved-concessions/approved-concessions.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ApprovedConcessionsComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var ApprovedConcessionsComponent = (function () {
    function ApprovedConcessionsComponent() {
    }
    ApprovedConcessionsComponent.prototype.ngOnInit = function () {
    };
    return ApprovedConcessionsComponent;
}());
ApprovedConcessionsComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-approved-concessions',
        template: __webpack_require__("../../../../../client-src/app/approved-concessions/approved-concessions.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/approved-concessions/approved-concessions.component.css")]
    }),
    __metadata("design:paramtypes", [])
], ApprovedConcessionsComponent);

//# sourceMappingURL=approved-concessions.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/conditions/conditions.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/conditions/conditions.component.html":
/***/ (function(module, exports) {

module.exports = "  <!-- Total widgets -->\r\n<div class=\"col-md-12\">\r\n  <div class=\"totalsWidget outer \">\r\n    <div class=\"activeWidget\">\r\n      <div class=\"cornered\">\r\n        <p>Standard</p>\r\n      </div>\r\n      <div class=\"main\">\r\n        <p>27</p>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <div class=\"totalsWidget outer\" style=\"margin-left: 20px;\">\r\n    <div class=\"cornered\">\r\n      <p>Ongoing</p>\r\n    </div>\r\n    <div class=\"main\">\r\n      <p>13</p>\r\n    </div>\r\n  </div>\r\n\r\n</div>\r\n<div class=\"col-md-12 search-and-results-container\">\r\n  <!-- Search bar -->\r\n  <div class=\"input-group add-on\">\r\n    <input class=\"form-control\" placeholder=\"Search Concession ID or Risk Group Number\" name=\"srch-term\" id=\"srch-term\" type=\"text\">\r\n  </div>\r\n  <!-- filter-->\r\n  <div class=\"form-group  filter-group\">\r\n    <div id=\"filter\"><label for=\"sort\" class=\"col-sm-1 control-label\"> Filter</label></div>\r\n    <div class=\"col-sm-2\">\r\n      <select class=\"form-control\">\r\n        <option>3 Months</option>\r\n        <option>6 Months</option>\r\n      </select>\r\n    </div>\r\n  </div>\r\n\r\n  <!-- Results table -->\r\n  <div class=\"table-container\">\r\n    <table class=\"table table-bordered table-hover header-fixed table-striped\">\r\n      <thead>\r\n        <tr>\r\n          <th>Risk Group Number</th>\r\n          <th>Customer</th>\r\n          <th>Concession ID</th>\r\n          <th>Condition Type</th>\r\n          <th>Expected Turnover</th>\r\n          <th>Interest</th>\r\n          <th>Volume</th>\r\n          <th>Value</th>\r\n          <th>Condition Met</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr class=\"status-red\">\r\n          <td>\r\n            <div class=\"customerName\">APPLE</div>\r\n            <div>1989</div>\r\n          </td>\r\n          <td>\r\n            <div>Mac</div>\r\n            <div>Cashman</div>\r\n          </td>\r\n          <td> L00000</td>\r\n          <td>\r\n            Full Transaction\r\n          </td>\r\n          <td>100 000000</td>\r\n          <td>0.5</td>\r\n          <td>\r\n            5000 00\r\n          </td>\r\n          <td>500 0000</td>\r\n          <td>\r\n            <button type=\"button\" class=\"btn btn-no\">No</button> <button type=\"button\" class=\"btn btn-yes\">Yes</button>\r\n          </td>\r\n        </tr>\r\n        <tr class=\"status-green\">\r\n          <td>\r\n            <div class=\"customerName\">EDCON</div>\r\n            <div>1989</div>\r\n          </td>\r\n          <td>\r\n            <div>CNA</div>\r\n            <div>overdraft</div>\r\n          </td>\r\n          <td> L00000</td>\r\n          <td>Cash Turnover</td>\r\n          <td>100 000000</td>\r\n          <td>0.5</td>\r\n          <td> 5000 00</td>\r\n          <td>500 0000</td>\r\n          <td>\r\n            <button type=\"button\" class=\"btn btn-no\">No</button> <button type=\"button\" class=\"btn btn-yes\">Yes</button>\r\n          </td>\r\n\r\n        </tr>\r\n        <tr class=\"status-yellow\">\r\n          <td>\r\n            <div class=\"customerName\">EDCON</div>\r\n            <div>1989</div>\r\n          </td>\r\n          <td>\r\n            <div>CNA</div>\r\n            <div>overdraft</div>\r\n          </td>\r\n          <td> L00000</td>\r\n          <td>Cash Turnover</td>\r\n          <td>100 000000</td>\r\n          <td>0.5</td>\r\n          <td> 5000 00</td>\r\n          <td>500 0000</td>\r\n          <td>\r\n            <button type=\"button\" class=\"btn btn-no\">No</button>\r\n            <button type=\"button\" class=\"btn btn-yes\">Yes</button>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n\r\n  <div class=\"concession-footer\">\r\n    <!--print button-->\r\n    <button type=\"button\" class=\"btn btn-print\">\r\n      <i class=\"fa fa-print\" aria-hidden=\"true\"></i> Print 1 Files\r\n    </button>\r\n    <!-- pagination-->\r\n    <ul class=\"pagination\">\r\n      <li><a href=\"#\">First</a></li>\r\n      <li><a href=\"#\">Prev</a></li>\r\n      <li class=\"active\"><a href=\"#\">1</a></li>\r\n      <li><a href=\"#\">2</a></li>\r\n      <li><a href=\"#\">Next</a></li>\r\n      <li><a href=\"#\">Last</a></li>\r\n    </ul>\r\n  </div>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/conditions/conditions.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ConditionsComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var ConditionsComponent = (function () {
    function ConditionsComponent() {
    }
    ConditionsComponent.prototype.ngOnInit = function () {
    };
    return ConditionsComponent;
}());
ConditionsComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-conditions',
        template: __webpack_require__("../../../../../client-src/app/conditions/conditions.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/conditions/conditions.component.css")]
    }),
    __metadata("design:paramtypes", [])
], ConditionsComponent);

//# sourceMappingURL=conditions.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/declined-inbox/declined-inbox.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/declined-inbox/declined-inbox.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n<app-inbox-header></app-inbox-header>\r\n\r\n<div class=\"col-md-12 search-and-results-container\">\r\n  <app-inbox-search-bar></app-inbox-search-bar>\r\n  <!-- Results table -->\r\n  <div class=\"table-container\">\r\n    <table class=\"table table-bordered table-hover header-fixed table-striped\">\r\n      <thead>\r\n        <tr>\r\n          <th>Risk Group</th>\r\n          <th>Customer Name</th>\r\n          <th>Type</th>\r\n          <th>Date Opened</th>\r\n          <th>Concession ID</th>\r\n          <th>Segment</th>\r\n          <th>Sent For Approval</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">APPLE</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>Mac</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L00000</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n\r\n        </tr>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">EDCON</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>CNA</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L11111</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <!-- pagination-->\r\n  <ul class=\"pagination\">\r\n    <li><a href=\"#\">First</a></li>\r\n    <li><a href=\"#\">Prev</a></li>\r\n    <li class=\"active\"><a href=\"#\">1</a></li>\r\n    <li><a href=\"#\">2</a></li>\r\n    <li><a href=\"#\">Next</a></li>\r\n    <li><a href=\"#\">Last</a></li>\r\n  </ul>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/declined-inbox/declined-inbox.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DeclinedInboxComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var DeclinedInboxComponent = (function () {
    function DeclinedInboxComponent() {
    }
    DeclinedInboxComponent.prototype.ngOnInit = function () {
    };
    return DeclinedInboxComponent;
}());
DeclinedInboxComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-declined-inbox',
        template: __webpack_require__("../../../../../client-src/app/declined-inbox/declined-inbox.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/declined-inbox/declined-inbox.component.css")]
    }),
    __metadata("design:paramtypes", [])
], DeclinedInboxComponent);

//# sourceMappingURL=declined-inbox.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/due-expiry-inbox/due-expiry-inbox.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/due-expiry-inbox/due-expiry-inbox.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n<app-inbox-header></app-inbox-header>\r\n\r\n<div class=\"col-md-12 search-and-results-container\">\r\n  <app-inbox-search-bar></app-inbox-search-bar>\r\n  <!-- Results table -->\r\n  <div class=\"table-container\">\r\n    <table class=\"table table-bordered table-hover header-fixed table-striped\">\r\n      <thead>\r\n        <tr>\r\n          <th>Risk Group</th>\r\n          <th>Customer Name</th>\r\n          <th>Type</th>\r\n          <th>Date Opened</th>\r\n          <th>Concession ID</th>\r\n          <th>Segment</th>\r\n          <th>Sent For Approval</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">APPLE</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>Mac</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L00000</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n\r\n        </tr>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">EDCON</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>CNA</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L11111</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <!-- pagination-->\r\n  <ul class=\"pagination\">\r\n    <li><a href=\"#\">First</a></li>\r\n    <li><a href=\"#\">Prev</a></li>\r\n    <li class=\"active\"><a href=\"#\">1</a></li>\r\n    <li><a href=\"#\">2</a></li>\r\n    <li><a href=\"#\">Next</a></li>\r\n    <li><a href=\"#\">Last</a></li>\r\n  </ul>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/due-expiry-inbox/due-expiry-inbox.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DueExpiryInboxComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var DueExpiryInboxComponent = (function () {
    function DueExpiryInboxComponent() {
    }
    DueExpiryInboxComponent.prototype.ngOnInit = function () {
    };
    return DueExpiryInboxComponent;
}());
DueExpiryInboxComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-due-expiry-inbox',
        template: __webpack_require__("../../../../../client-src/app/due-expiry-inbox/due-expiry-inbox.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/due-expiry-inbox/due-expiry-inbox.component.css")]
    }),
    __metadata("design:paramtypes", [])
], DueExpiryInboxComponent);

//# sourceMappingURL=due-expiry-inbox.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/expired-inbox/expired-inbox.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/expired-inbox/expired-inbox.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n<app-inbox-header></app-inbox-header>\r\n\r\n<div class=\"col-md-12 search-and-results-container\">\r\n  <app-inbox-search-bar></app-inbox-search-bar>\r\n  <!-- Results table -->\r\n  <div class=\"table-container\">\r\n    <table class=\"table table-bordered table-hover header-fixed table-striped\">\r\n      <thead>\r\n        <tr>\r\n          <th>Risk Group</th>\r\n          <th>Customer Name</th>\r\n          <th>Type</th>\r\n          <th>Date Opened</th>\r\n          <th>Concession ID</th>\r\n          <th>Segment</th>\r\n          <th>Sent For Approval</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">APPLE</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>Mac</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L00000</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n\r\n        </tr>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">EDCON</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>CNA</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L11111</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <!-- pagination-->\r\n  <ul class=\"pagination\">\r\n    <li><a href=\"#\">First</a></li>\r\n    <li><a href=\"#\">Prev</a></li>\r\n    <li class=\"active\"><a href=\"#\">1</a></li>\r\n    <li><a href=\"#\">2</a></li>\r\n    <li><a href=\"#\">Next</a></li>\r\n    <li><a href=\"#\">Last</a></li>\r\n  </ul>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/expired-inbox/expired-inbox.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ExpiredInboxComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var ExpiredInboxComponent = (function () {
    function ExpiredInboxComponent() {
    }
    ExpiredInboxComponent.prototype.ngOnInit = function () {
    };
    return ExpiredInboxComponent;
}());
ExpiredInboxComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-expired-inbox',
        template: __webpack_require__("../../../../../client-src/app/expired-inbox/expired-inbox.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/expired-inbox/expired-inbox.component.css")]
    }),
    __metadata("design:paramtypes", [])
], ExpiredInboxComponent);

//# sourceMappingURL=expired-inbox.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/inbox-header/inbox-header.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/inbox-header/inbox-header.component.html":
/***/ (function(module, exports) {

module.exports = "  <!-- Total widgets -->\r\n<div class=\"col-md-12\">\r\n  <div class=\"totalsWidget outer\">\r\n    <div routerLink=\"/pending-inbox\" routerLinkActive=\"activeWidget\">\r\n      <div class=\"cornered\"><p>Pending</p></div>\r\n      <div class=\"main\"><p>16</p></div>\r\n    </div>\r\n  </div>\r\n  <div class=\"totalsWidget outer\" style=\"margin-left: 20px;\">\r\n    <div routerLink=\"/due-expiry-inbox\" routerLinkActive=\"activeWidget\">\r\n      <div class=\"cornered\"><p>Due For Expiry</p></div>\r\n      <div class=\"main\"><p>2</p></div>\r\n    </div>\r\n  </div>\r\n  <div class=\"totalsWidget outer\" style=\"margin-left: 20px;\">\r\n    <div routerLink=\"/expired-inbox\" routerLinkActive=\"activeWidget\">\r\n      <div class=\"cornered\"><p>Expired</p></div>\r\n      <div class=\"main\"><p>10</p></div>\r\n    </div>\r\n  </div>\r\n  <div class=\"totalsWidget outer\" style=\"margin-left: 20px;\">\r\n    <div routerLink=\"/declined-inbox\" routerLinkActive=\"activeWidget\">\r\n      <div class=\"cornered\"><p>Declined</p></div>\r\n      <div class=\"main\"><p>4</p></div>\r\n    </div>\r\n  </div>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/inbox-header/inbox-header.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return InboxHeaderComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var InboxHeaderComponent = (function () {
    function InboxHeaderComponent() {
    }
    InboxHeaderComponent.prototype.ngOnInit = function () {
    };
    return InboxHeaderComponent;
}());
InboxHeaderComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-inbox-header',
        template: __webpack_require__("../../../../../client-src/app/inbox-header/inbox-header.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/inbox-header/inbox-header.component.css")]
    }),
    __metadata("design:paramtypes", [])
], InboxHeaderComponent);

//# sourceMappingURL=inbox-header.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/inbox-search-bar/inbox-search-bar.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/inbox-search-bar/inbox-search-bar.component.html":
/***/ (function(module, exports) {

module.exports = "<!-- Search bar -->\r\n<div class=\"input-group add-on\">\r\n  <input class=\"form-control\" placeholder=\"Search Concession ID or Risk Group Number\" name=\"srch-term\" id=\"srch-term\" type=\"text\">\r\n  <div class=\"input-group-btn\">\r\n    <button class=\"btn btn-default-search\" type=\"submit\"><i class=\"glyphicon glyphicon-search\"></i></button>\r\n  </div>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/inbox-search-bar/inbox-search-bar.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return InboxSearchBarComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var InboxSearchBarComponent = (function () {
    function InboxSearchBarComponent() {
    }
    InboxSearchBarComponent.prototype.ngOnInit = function () {
    };
    return InboxSearchBarComponent;
}());
InboxSearchBarComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-inbox-search-bar',
        template: __webpack_require__("../../../../../client-src/app/inbox-search-bar/inbox-search-bar.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/inbox-search-bar/inbox-search-bar.component.css")]
    }),
    __metadata("design:paramtypes", [])
], InboxSearchBarComponent);

//# sourceMappingURL=inbox-search-bar.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/page-header/page-header.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/page-header/page-header.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"col-md-12 header\">\r\n  <div class=\"logo\"></div>\r\n</div>\r\n<div class=\"col-md-12 nav-pills-container\">\r\n  <ul class=\"nav nav-pills\">\r\n    <li routerLinkActive=\"selected-nav-item\">\r\n      <a routerLink=\"/pending-inbox\">Inbox</a>\r\n      <!-- The following are here so that the router link active class is enabled for any of the inbox routes-->\r\n      <a routerLink=\"/due-expiry-inbox\" style=\"display: none;\">Inbox</a>\r\n      <a routerLink=\"/expired-inbox\" style=\"display: none;\">Inbox</a>\r\n      <a routerLink=\"/declined-inbox\" style=\"display: none;\">Inbox</a>\r\n    </li>\r\n    <li routerLinkActive=\"selected-nav-item\"><a routerLink=\"/approved-concessions\">Approved Concessions</a></li>\r\n    <li routerLinkActive=\"selected-nav-item\"><a routerLink=\"/conditions\">Conditions</a></li>\r\n    <li routerLinkActive=\"selected-nav-item\"><a routerLink=\"/pricing\">Pricing</a></li>\r\n    <li class=\"logout-li\"><a href=\"#\"><span class=\"glyphicon glyphicon-log-out\"></span> Logout</a></li>\r\n  </ul>\r\n</div> "

/***/ }),

/***/ "../../../../../client-src/app/page-header/page-header.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PageHeaderComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var PageHeaderComponent = (function () {
    function PageHeaderComponent() {
    }
    PageHeaderComponent.prototype.ngOnInit = function () {
    };
    return PageHeaderComponent;
}());
PageHeaderComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-page-header',
        template: __webpack_require__("../../../../../client-src/app/page-header/page-header.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/page-header/page-header.component.css")]
    }),
    __metadata("design:paramtypes", [])
], PageHeaderComponent);

//# sourceMappingURL=page-header.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/pending-inbox/pending-inbox.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/pending-inbox/pending-inbox.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n<app-inbox-header></app-inbox-header>\r\n\r\n<div class=\"col-md-12 search-and-results-container\">\r\n  <app-inbox-search-bar></app-inbox-search-bar>\r\n  <!-- Results table -->\r\n  <div class=\"table-container\">\r\n    <table class=\"table table-bordered table-hover header-fixed table-striped\">\r\n      <thead>\r\n        <tr>\r\n          <th>Risk Group</th>\r\n          <th>Customer Name</th>\r\n          <th>Type</th>\r\n          <th>Date Opened</th>\r\n          <th>Concession ID</th>\r\n          <th>Segment</th>\r\n          <th>Sent For Approval</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">APPLE</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>Mac</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L00000</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n\r\n        </tr>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">EDCON</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>CNA</td>\r\n          <td>Transactional</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>L11111</td>\r\n          <td>Expert</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n\r\n        </tr>\r\n        <tr>\r\n          <td>\r\n            <p class=\"customerName\">VIRGIN</p>\r\n            <p class=\"date\">1989</p>\r\n          </td>\r\n          <td>Virgin Money</td>\r\n          <td>BOL</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n          <td>B309864</td>\r\n          <td>Franchising</td>\r\n          <td>\r\n            <p class=\"date\">2017/05/02 </p>\r\n          </td>\r\n\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <!-- pagination-->\r\n  <ul class=\"pagination\">\r\n    <li><a href=\"#\">First</a></li>\r\n    <li><a href=\"#\">Prev</a></li>\r\n    <li class=\"active\"><a href=\"#\">1</a></li>\r\n    <li><a href=\"#\">2</a></li>\r\n    <li><a href=\"#\">Next</a></li>\r\n    <li><a href=\"#\">Last</a></li>\r\n  </ul>\r\n</div>"

/***/ }),

/***/ "../../../../../client-src/app/pending-inbox/pending-inbox.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PendingInboxComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var PendingInboxComponent = (function () {
    function PendingInboxComponent() {
    }
    PendingInboxComponent.prototype.ngOnInit = function () {
    };
    return PendingInboxComponent;
}());
PendingInboxComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-pending-inbox',
        template: __webpack_require__("../../../../../client-src/app/pending-inbox/pending-inbox.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/pending-inbox/pending-inbox.component.css")]
    }),
    __metadata("design:paramtypes", [])
], PendingInboxComponent);

//# sourceMappingURL=pending-inbox.component.js.map

/***/ }),

/***/ "../../../../../client-src/app/pricing/pricing.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../client-src/app/pricing/pricing.component.html":
/***/ (function(module, exports) {

module.exports = "  <!-- banner-->\r\n<div class=\"col-md-12 pricing-banner\">\r\n  <div class=\"col-md-1 pricing-user-image\">\r\n    <i class=\"fa fa-user-o\" aria-hidden=\"true\"></i>\r\n  </div>\r\n  <div class=\"col-md-3\">\r\n    <div class=\"pricing-form\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-5\">\r\n          <p class=\"lightTitle\">Region</p>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n          <p>Some Region</p>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-5\">\r\n          <p class=\"lightTitle\">Business Unit</p>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n          <p>Cape Town</p>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <div class=\"col-md-4\">\r\n    <div class=\"pricing-form\">\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-3\">\r\n          <p class=\"lightTitle\">Segment</p>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n          <p>Expert</p>\r\n        </div>\r\n      </div>\r\n      <div class=\"row\">\r\n        <div class=\"col-sm-3\">\r\n          <p class=\"lightTitle\">Province</p>\r\n        </div>\r\n        <div class=\"col-sm-6\">\r\n          <p>Northen Cape</p>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n<div class=\"col-md-12 search-and-results-container\">\r\n  <!-- Search bar -->\r\n  <div class=\"input-group add-on\">\r\n    <input class=\"form-control\" placeholder=\"Risk Group Number\" name=\"srch-term\" id=\"srch-term\" type=\"text\" onclick=\"location.href='pricing-results.html'\">\r\n    <div class=\"input-group-btn\">\r\n      <!-- updated search bar button -->\r\n      <button class=\"btn btn-default-search\" type=\"submit\">Search</button>\r\n    </div>\r\n  </div>\r\n</div>\r\n<!-- Risk Group details -->\r\n<div class=\"col-md-12\">\r\n  <div class=searchEmptyState>\r\n    <i class=\"fa fa-info-circle\" aria-hidden=\"true\"></i>\r\n    <div>\r\n      Enter risk group number to return customer products\r\n    </div>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../client-src/app/pricing/pricing.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PricingComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var PricingComponent = (function () {
    function PricingComponent() {
    }
    PricingComponent.prototype.ngOnInit = function () {
    };
    return PricingComponent;
}());
PricingComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* Component */])({
        selector: 'app-pricing',
        template: __webpack_require__("../../../../../client-src/app/pricing/pricing.component.html"),
        styles: [__webpack_require__("../../../../../client-src/app/pricing/pricing.component.css")]
    }),
    __metadata("design:paramtypes", [])
], PricingComponent);

//# sourceMappingURL=pricing.component.js.map

/***/ }),

/***/ "../../../../../client-src/environments/environment.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
// The file contents for the current environment will overwrite these during build.
var environment = {
    production: false
};
//# sourceMappingURL=environment.js.map

/***/ }),

/***/ "../../../../../client-src/main.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__("../../../platform-browser-dynamic/@angular/platform-browser-dynamic.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_app_module__ = __webpack_require__("../../../../../client-src/app/app.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__("../../../../../client-src/environments/environment.ts");




if (__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].production) {
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["a" /* enableProdMode */])();
}
__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_2__app_app_module__["a" /* AppModule */]);
//# sourceMappingURL=main.js.map

/***/ }),

/***/ 1:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../client-src/main.ts");


/***/ })

},[1]);
//# sourceMappingURL=main.bundle.js.map