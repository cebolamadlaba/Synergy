var app = angular.module("myApp");

app.directive("motivation", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/motivation.html"
    };
}]);

