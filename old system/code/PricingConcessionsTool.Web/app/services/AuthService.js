//angular.module('AuthServices', ['ngResource'])


//   .factory('Auth', ['$resource', '$rootScope', function ($resource, $rootScope)


//   {

//       var baseUrl = $("base").first().attr("href");

//       return $resource(baseUrl + 'api/user/getuserprofile');
//   }]
//   );

var myApp = angular.module('myApp');

myApp.service('sharedDataService', ['$window', function ($window)
{
        var sharedDataService ={};

        sharedDataService.getUser = function ()
        {

            var userfromJson = $window.sessionStorage.getItem("user");

            return angular.fromJson(userfromJson);
        }

        sharedDataService.setUser = function (jsonData)
        {
            var user = angular.toJson(jsonData);

            $window.sessionStorage.setItem("user", user);
        }

         sharedDataService.Customer = {}

        return sharedDataService;
}]);

