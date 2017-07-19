/// <reference path="../templates/complaintControl.html" />


var app = angular.module("myApp");

app.directive("masConcession", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Mas/concession.html"
    };
}]);

app.directive("lendingConcession", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Lending/concession.html"
    };
}]);

app.directive("tradeConcession", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Trade/concession.html"
    };
}]);


app.directive("investmentConcession", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/investment/concession.html"
    };
}]);



app.directive("transactionalConcession", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Transactional/concession.html"
    };
}]);


app.directive("cashConcession", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/cash/concession.html"
    };
}]);



app.directive("bolConcession", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/bol/concession.html"
    };
}]);


