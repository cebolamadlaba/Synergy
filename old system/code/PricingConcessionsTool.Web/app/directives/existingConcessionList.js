/// <reference path="../templates/complaintControl.html" />


var app = angular.module("myApp");

app.directive("existingConcessionList", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/existingConcessionList.html"
    };
}]);

