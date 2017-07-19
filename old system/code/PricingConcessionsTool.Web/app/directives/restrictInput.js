


(function () {
    'use strict';
    var myApp = angular.module('myApp');

    myApp.directive('restrictInput', [function () {

        var pattern = "";
        var decimalOnlypattern = "^[0-9-]+(\.\d{1,2})?";
        var numberOnlypattern = "^[0-9-]*$";

        return {
            restrict: 'A',
            link: function (scope, element, attrs) {

                if (attrs.restrictInput == "Number") {
                    pattern = numberOnlypattern;
                }
                else if (attrs.restrictInput == "Decimal") {
                    pattern = decimalOnlypattern;
                }

                var ele = element[0];
                var regex = RegExp(pattern);
                var value = ele.value;

                ele.addEventListener('keyup', function (e) {
                    if (regex.test(ele.value)) {
                        value = ele.value;
                    } else {
                        ele.value = value;
                    }
                });

            }
        };
    }]);


    myApp.directive('format', ['$filter', function ($filter)
    {
        return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
                if (!ctrl) return;

                ctrl.$formatters.unshift(function (a) {
                    return $filter(attrs.format)(ctrl.$modelValue)
                });

                elem.bind('blur', function(event) {
                    var plainNumber = elem.val().replace(/[^\d|\-+|\.+]/g, '');
                    elem.val($filter(attrs.format)(plainNumber));
                });
            }
        };
    }]);
   
})();

