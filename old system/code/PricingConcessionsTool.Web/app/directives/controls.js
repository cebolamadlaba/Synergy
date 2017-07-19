/// <reference path="../templates/complaintControl.html" />


var app = angular.module("myApp");

app.directive("customerSummary", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/customerSummary.html"
    };
}]);


app.directive("customerSummaryColumn", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/customerSummaryColumn.html"
    };
}]);

