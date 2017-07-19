/// <reference path="../templates/complaintControl.html" />


var app = angular.module("myApp");

app.directive("conditionList", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/conditionList.html"
    };
}]);

