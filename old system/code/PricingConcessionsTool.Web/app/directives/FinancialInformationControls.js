var app = angular.module("myApp");

app.directive("financialInfoLending", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/lending/FinancialInfoLending.html"
    };
}]);

app.directive("financialInfoInvestment", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Investment/FinancialInfoInvestment.html"
    };
}]);


app.directive("financialInfoMas", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Mas/FinancialInfoMas.html"
    };
}]);


app.directive("financialInfoTrade", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Trade/FinancialInfoTrade.html"
    };
}]);


app.directive("financialInfoTransactional", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Transactional/FinancialInfoTransactional.html"
    };
}]);



app.directive("financialInfoCash", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Cash/FinancialInfoCash.html"
    };
}]);



app.directive("financialInfoBol", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/Bol/FinancialInfoBol.html"
    };
}]);
