
var myApp = angular.module('myApp');

//ngular.module('selectFilters', ['filters']);

//angular.module('filters', [])

myApp.filter('selectSpecialistFilter', [function ()
{
    return function (incItems, value)
    {
        var out = [];

        if (value) {
            for (x = 0; x < incItems.length; x++)
            {
                if (incItems[x].UserId != value)
                    out.push(incItems[x]);
            }
            return out;
        }
        else if (!value) {
            return incItems
        }
    };
}]);


myApp.directive('toggle', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            if (attrs.toggle == "tooltip") {
                $(element).tooltip();
            }
            if (attrs.toggle == "popover") {
                $(element).popover();
            }
        }
    };
})

myApp.directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if ('disabled' in attrs) {
                elem.on('click', function (e) {
                    e.preventDefault(); // prevent link click
                });
            }
        }
    };
});



myApp.directive('yourDirective', function () {
    return {
        link: function (scope, element, attrs) {
            element.multiselect({
                buttonClass: 'btn',
                buttonWidth: 'auto',
                buttonContainer: '<div class="btn-group" />',
                maxHeight: false,
                buttonText: function(options) {
                    if (options.length == 0) {
                        return 'None selected <b class="caret"></b>';
                    }
                    else if (options.length > 3) {
                        return options.length + ' selected  <b class="caret"></b>';
                    }
                    else {
                        var selected = '';
                        options.each(function() {
                            selected += $(this).text() + ', ';
                        });
                        return selected.substr(0, selected.length -2) + ' <b class="caret"></b>';
                    }
                }
            });

            // Watch for any changes to the length of our select element
            scope.$watch(function () {
                return element[0].length;
            }, function () {
                element.multiselect('rebuild');
            });

            // Watch for any changes from outside the directive and refresh
            scope.$watch(attrs.ngModel, function () {
                element.multiselect('refresh');
            });

        }

    };
});
