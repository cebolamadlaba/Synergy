var app = angular.module("myApp");

app.directive("approvalInfo", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/approvalInfo.html"
    };
}]);