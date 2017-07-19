var myApp = angular.module("myApp");

myApp.config(['toastrConfig', function (toastrConfig) {
    angular.extend(toastrConfig, {
        closeButton: true,
        autoDismiss: false,
        containerId: 'toast-container',
        maxOpened: 0,
        newestOnTop: true,
        positionClass: 'toast-top-right',
        preventDuplicates: false,
        preventOpenDuplicates: false,
        target: 'body'
    });
}]);


