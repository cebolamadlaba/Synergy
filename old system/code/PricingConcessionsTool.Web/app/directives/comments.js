var app = angular.module("myApp");

app.directive("comments", ['$rootScope', function ($rootScope) {
    return {
        restrict: "E",
        templateUrl: $rootScope.BaseUrl + "app/templates/concessions/comments.html"
    };
}]);