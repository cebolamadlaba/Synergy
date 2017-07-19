/// <reference path="../templates/complaintControl.html" />


var app = angular.module("myApp");

app.directive("tableSearchControl", ['$rootScope', function ($rootScope)
{
    return {
        restrict : "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/tableSearchControl.html"
    };
}]);


app.directive("checkConcessionsOtherApprover", ['$rootScope', function ($rootScope)
{
    return {
        restrict : "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/checkConcessionsOtherApprover.html"
    };
}]);



