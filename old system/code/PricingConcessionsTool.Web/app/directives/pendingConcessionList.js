/// <reference path="../templates/complaintControl.html" />


var app = angular.module("myApp");

app.directive("pendingConcessionList", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/pendingConcessionList.html"
    };
}]);

