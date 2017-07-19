

(function () {
    'use strict';


    var myApp = angular.module('myApp', ['ui.router',
                                        'ui.bootstrap',
                                        'ngSanitize',
                                        'ui.bootstrap.datetimepicker',
                                        'toastr',
                                        'btorfs.multiselect',
                                        'angularSpinner',
                                        'ngAnimate',
                                        'ngMessages',
                                        'angularUtils.directives.dirPagination']);


    myApp.run(['$rootScope', '$location',function ($rootScope, $location) {

        var host = $location.host();

        var url = 'http://' + location.host + '/PricingConcessionsTool/';

        $rootScope.BaseUrl = url;

        $rootScope.$on('$stateChangeStart', function (event, next, absOldUrl) {

            $rootScope.$emit('load');

            $rootScope.ConditionList = [];

        });

        $rootScope.$on('$stateChangeSuccess', function (event, to, toParams, from, fromParams) {
            $rootScope.previousState = from.name;
            $rootScope.fromParams = fromParams
            $rootScope.$emit('unload');
        });

        $rootScope.$on('$stateChangeError', function () {
            $rootScope.$emit('unload');
        });
    }]);


    var serviceBase = 'http://localhost:24888/PricingConcessionsTool/api/api/';
    //var serviceBase = 'http://localhost:24876/api/'

    myApp.constant('AppSettings', {
        apiServiceBaseUri: serviceBase,
    });

  

    myApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/login');

        var loginState =
           {
               name: 'login',
               url: '/login',
               templateUrl: 'app/templates/login.html',
               controller: 'loginController',
           }

        var mypendingState =
        {
            name: 'mypending',
            url: '/concessions/mypending',
            templateUrl: 'app/templates/concessions/mypendingconcession.html',
            controller: 'pendingController',
        }

        var myApprovedConcessionControllerState =
        {
            name: 'myapprovedconcessions',
            url: '/concessions/myapprovedconcessions',
            templateUrl: 'app/templates/concessions/myapprovedconcession.html',
            controller: 'myApprovedConcessionController',
        }

        var myactionedconcessionControllerState =
        {
            name: 'myactionedconcessions',
            url: '/concessions/myactionedconcessions',
            templateUrl: 'app/templates/concessions/myactionedconcession.html',
            controller: 'myactionedconcessionController',
        }

        var mypricingState =
        {
            name: 'mypricing',
            url: '/concessions/mypricing',
            templateUrl: 'app/templates/concessions/mypricing.html',
            controller: 'mypricingController',
        }
        
        var approvalState =
        {
            name: 'Approval',
            url: '/concessions/approval',
            params: { ConcessionId: null, },
            templateUrl: 'app/templates/concessions/approval.html',
            controller: 'approvalController',
        }

        var approvalLendingState =
        {
            name: 'Approval.Lending',
            templateUrl: 'app/templates/concessions/lending/concession.html',
            controller: 'approvalLendingController',
        }


        var approvalInvestmentState =
        {
            name: 'Approval.Investment',
            templateUrl: 'app/templates/concessions/Investment/concession.html',
            controller: 'approvalInvestmentController',
        }

        var approvalMasState =
        {
            name: 'Approval.Mas',
            templateUrl: 'app/templates/concessions/Mas/concession.html',
            controller: 'approvalMasController',
        }

        var approvalTradeState =
        {
            name: 'Approval.Trade',
            templateUrl: 'app/templates/concessions/Trade/concession.html',
            controller: 'approvalTradeController',
        }

        var approvalBolState =
        {
            name: 'Approval.Bol',
            templateUrl: 'app/templates/concessions/Bol/concession.html',
            controller: 'approvalBolController',
        }

        var approvalCashState =
        {
            name: 'Approval.Cash',
            templateUrl: 'app/templates/concessions/Cash/concession.html',
            controller: 'approvalCashController',
        }


        var approvalTransactionalState =
        {
            name: 'Approval.Transactional',
            templateUrl: 'app/templates/concessions/Transactional/concession.html',
            controller: 'approvalTransactionalController',
        }

        ////////////////////////////////// Lending ////////////////////////////////////////////////

        var lendingNewState =
      {
          name: 'lendingNew',
          url: '/concessions/lending/new',
          templateUrl: 'app/templates/concessions/lending/new.html',
          controller: 'newLendingController',
      }


        var lendingViewState =
        {
            name: 'lendingView',
            url: '/concessions/lending/view',
            templateUrl: 'app/templates/concessions/lending/view.html',
            controller: 'viewLendingController',
        }

        var lendingEditState =
       {
           name: 'EditLending',
           url: '/concessions/lending/edit',
           params: { ConcessionId: null, },
           templateUrl: 'app/templates/concessions/lending/edit.html',
           controller: 'editLendingController',
       }

        ////////////////////////////////// Lending ////////////////////////////////////////////////


        ////////////////////////////////// Investment ////////////////////////////////////////////////


        var InvestmentNewState =
        {
            name: 'InvestmentNew',
            url: '/concessions/Investment/new',
            templateUrl: 'app/templates/concessions/Investment/new.html',
            controller: 'newInvestmentController',
        }

        var InvestmentEditState =
         {
             name: 'EditInvestment',
             url: '/concessions/Investment/edit',
             params: { ConcessionId: null, },
             templateUrl: 'app/templates/concessions/Investment/edit.html',
             controller: 'editInvestmentController',
         }



        var InvestmentViewState =
        {
            name: 'InvestmentView',
            url: '/concessions/Investment/view',
            templateUrl: 'app/templates/concessions/Investment/view.html',
            controller: 'viewInvestmentController',
        }



        ////////////////////////////////// Investment ////////////////////////////////////////////////



        ////////////////////////////////// MAS ////////////////////////////////////////////////


        var MasNewState =
        {
            name: 'MasNew',
            url: '/concessions/Mas/new',
            templateUrl: 'app/templates/concessions/Mas/new.html',
            controller: 'newMasController',
        }

        var MasEditState =
         {
             name: 'EditMas',
             url: '/concessions/Mas/edit',
             params: { ConcessionId: null, },
             templateUrl: 'app/templates/concessions/Mas/edit.html',
             controller: 'editMasController',
         }



        var MasViewState =
        {
            name: 'MasView',
            url: '/concessions/Mas/view',
            templateUrl: 'app/templates/concessions/Mas/view.html',
            controller: 'viewMasController',
        }



        ////////////////////////////////// MAS ////////////////////////////////////////////////


        ////////////////////////////////// BOL ////////////////////////////////////////////////


        var BolNewState =
        {
            name: 'BolNew',
            url: '/concessions/Bol/new',
            templateUrl: 'app/templates/concessions/Bol/new.html',
            controller: 'newBolController',
        }

        var BolEditState =
         {
             name: 'EditBol',
             url: '/concessions/Bol/edit',
             params: { ConcessionId: null, },
             templateUrl: 'app/templates/concessions/Bol/edit.html',
             controller: 'editBolController',
         }



        var BolViewState =
        {
            name: 'BolView',
            url: '/concessions/Bol/view',
            templateUrl: 'app/templates/concessions/Bol/view.html',
            controller: 'viewBolController',
        }



        ////////////////////////////////// BOL ////////////////////////////////////////////////


        ////////////////////////////////// Trade ////////////////////////////////////////////////


        var TradeNewState =
        {
            name: 'TradeNew',
            url: '/concessions/Trade/new',
            templateUrl: 'app/templates/concessions/Trade/new.html',
            controller: 'newTradeController',
        }

        var TradeEditState =
         {
             name: 'EditTrade',
             url: '/concessions/Trade/edit',
             params: { ConcessionId: null, },
             templateUrl: 'app/templates/concessions/Trade/edit.html',
             controller: 'editTradeController',
         }



        var TradeViewState =
        {
            name: 'TradeView',
            url: '/concessions/Trade/view',
            templateUrl: 'app/templates/concessions/Trade/view.html',
            controller: 'viewTradeController',
        }

        ////////////////////////////////// Trade ////////////////////////////////////////////////


        ////////////////////////////////// Transactional ////////////////////////////////////////////////


        var TransactionalNewState =
        {
            name: 'TransactionalNew',
            url: '/concessions/Transactional/new',
            templateUrl: 'app/templates/concessions/Transactional/new.html',
            controller: 'newTransactionalController',
        }

        var TransactionalEditState =
         {
             name: 'EditTransactional',
             url: '/concessions/Transactional/edit',
             params: { ConcessionId: null, },
             templateUrl: 'app/templates/concessions/Transactional/edit.html',
             controller: 'editTransactionalController',
         }



        var TransactionalViewState =
        {
            name: 'TransactionalView',
            url: '/concessions/Transactional/view',
            templateUrl: 'app/templates/concessions/Transactional/view.html',
            controller: 'viewTransactionalController',
        }

        ////////////////////////////////// Transactional ////////////////////////////////////////////////





        ////////////////////////////////// Cash ////////////////////////////////////////////////


        var CashNewState =
        {
            name: 'CashNew',
            url: '/concessions/Cash/new',
            templateUrl: 'app/templates/concessions/Cash/new.html',
            controller: 'newCashController',
        }

        var CashEditState =
         {
             name: 'EditCash',
             url: '/concessions/Cash/edit',
             params: { ConcessionId: null, },
             templateUrl: 'app/templates/concessions/Cash/edit.html',
             controller: 'editCashController',
         }



        var CashViewState =
        {
            name: 'CashView',
            url: '/concessions/Cash/view',
            templateUrl: 'app/templates/concessions/Cash/view.html',
            controller: 'viewCashController',
        }

        ////////////////////////////////// Cash ////////////////////////////////////////////////




        $stateProvider.state(loginState);
        $stateProvider.state(mypendingState);
        $stateProvider.state(myApprovedConcessionControllerState);
        $stateProvider.state(mypricingState);
        $stateProvider.state(myactionedconcessionControllerState);

        $stateProvider.state(lendingNewState);
        $stateProvider.state(lendingViewState);
        $stateProvider.state(lendingEditState);


        $stateProvider.state(InvestmentNewState);
        $stateProvider.state(InvestmentEditState);
        $stateProvider.state(InvestmentViewState);

        $stateProvider.state(MasNewState);
        $stateProvider.state(MasEditState);
        $stateProvider.state(MasViewState);

        $stateProvider.state(TradeNewState);
        $stateProvider.state(TradeEditState);
        $stateProvider.state(TradeViewState);


        $stateProvider.state(BolNewState);
        $stateProvider.state(BolEditState);
        $stateProvider.state(BolViewState);

        $stateProvider.state(TransactionalNewState);
        $stateProvider.state(TransactionalEditState);
        $stateProvider.state(TransactionalViewState);


        $stateProvider.state(CashNewState);
        $stateProvider.state(CashEditState);
        $stateProvider.state(CashViewState);


        $stateProvider.state(approvalState);
        $stateProvider.state(approvalLendingState);
        $stateProvider.state(approvalInvestmentState);
        $stateProvider.state(approvalMasState);
        $stateProvider.state(approvalTradeState);
        $stateProvider.state(approvalBolState);
        $stateProvider.state(approvalCashState);
        $stateProvider.state(approvalTransactionalState);



        var approvalWithChanges =
            {
                name: 'ApprovalWithChanges',
                url: '/concessions/approvalWithChanges',
                params: { ConcessionId: null, },
                templateUrl: 'app/templates/concessions/approvalWithChanges.html',
                controller: 'approvalWithChangesController'
            }


        var approvalWithChangesBol =
           {
               name: 'ApprovalWithChanges.Bol',
               templateUrl: 'app/templates/concessions/Bol/concession.html',
               controller: 'approvalBolController',
           }


        var approvalWithChangesLending =
             {
                 name: 'ApprovalWithChanges.Lending',
                 templateUrl: 'app/templates/concessions/lending/concession.html',
                 controller: 'approvalLendingController',
             }


        var approvalWithChangesInvestment =
        {
            name: 'ApprovalWithChanges.Investment',
            templateUrl: 'app/templates/concessions/Investment/concession.html',
            controller: 'approvalInvestmentController',
        }

        var approvalWithChangesMas =
        {
            name: 'ApprovalWithChanges.Mas',
            templateUrl: 'app/templates/concessions/Mas/concession.html',
            controller: 'approvalMasController',
        }

        var approvalWithChangesTrade =
        {
            name: 'ApprovalWithChanges.Trade',
            templateUrl: 'app/templates/concessions/Trade/concession.html',
            controller: 'approvalTradeController',
        }

        var approvalWithChangesCash =
        {
            name: 'ApprovalWithChanges.Cash',
            templateUrl: 'app/templates/concessions/Cash/concession.html',
            controller: 'approvalCashController',
        }


        var approvalWithChangesTransactional =
        {
            name: 'ApprovalWithChanges.Transactional',
            templateUrl: 'app/templates/concessions/Transactional/concession.html',
            controller: 'approvalTransactionalController',
        }


        $stateProvider.state(approvalWithChanges);
        $stateProvider.state(approvalWithChangesBol);
        $stateProvider.state(approvalWithChangesLending);
        $stateProvider.state(approvalWithChangesInvestment);
        $stateProvider.state(approvalWithChangesMas);
        $stateProvider.state(approvalWithChangesTrade);
        $stateProvider.state(approvalWithChangesCash);
        $stateProvider.state(approvalWithChangesTransactional);







    //    myApp.constant('formatFactory', {
    //        currency: {
    //            pattern: /^[\(\-\+]?\d*,?\d*\.?\d+[\)]?$/,
    //            patternError: 'This field must be formatted as Currency.<br/>A sample valid input looks like: 1000.00',
    //            replace: /[,\$]/g,
    //            symbol: '$'
    //        },
    //        percent: {
    //            pattern: /^\d+(\.\d+)?$/,
    //            patternError: 'The value you entered is not a valid percentage.',
    //            replace: /[,%]/g,
    //            symbol: '%'
    //        },
    //        phone: {
    //            pattern: /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/,
    //            patternError: 'This field must be formatted as a Phone Number.<br/>A sample valid input looks like 123-456-7890',
    //            replace: /[-\.\(\)\sa-zA-Z]/g
    //        }
    //    })
    //.filter('percent', function () {
    //    return function (input) {
    //        if (!input || isNaN(input)) {
    //            return;
    //        } else {
    //            return input + '%';
    //        }
    //    };
    //})
    //.filter('phone', function () {
    //    'use strict';

    //    return function (input, parens, separator) {
    //        var out,
    //        arrOut,
    //        i;

    //        if (!input) {
    //            return;
    //        } else {
    //            if (!isNaN(input)) {
    //                input = input.toString();
    //            }
    //            separator = separator || '-';
    //            input = input.replace(' ', '');
    //            input = input.replace('(', '');
    //            input = input.replace(')', '');
    //            input = input.replace('.', '');

    //            arrOut = input.split('');

    //            // Loop through the array and remove anything that's NaN
    //            for (i = 0; i < arrOut.length; i++) {
    //                if (isNaN(parseInt(arrOut[i], 10))) {
    //                    arrOut.splice(i, 1);
    //                }
    //            }

    //            // Put all the separators back
    //            arrOut.splice(0, 0, '(');
    //            arrOut.splice(4, 0, ') ');
    //            arrOut.splice(8, 0, separator);
    //            out = arrOut.join('');

    //            return out;
    //        }
    //    };
    //})
    //.directive('quickInput', function (formatFactory) {
    //    return {
    //        scope: true,
    //        replace: true,
    //        transclude: true,
    //        template: '<div class="form-group"><label for="{{id}}">{{label}}</label><div ng-transclude></div><ul ng-if="modelCtrl.$invalid && modelCtrl.$touched"><li class="error" ng-if="modelCtrl.$error.required || modelCtrl.$error.parse">This field is required.</li><li class="error" ng-show="modelCtrl.$error.pattern && format.patternError">{{format.patternError}}</li></ul></div>',
    //        controller: function ($scope) {
    //            this.setFormatting = function (format) {
    //                $scope.format = format;
    //            };

    //            this.setLabel = function (label) {
    //                $scope.label = label;
    //            };

    //            this.setId = function (id) {
    //                $scope.id = id;
    //            };

    //            this.setModelCtrl = function (modelCtrl) {
    //                $scope.modelCtrl = modelCtrl;
    //            };
    //        }
    //    };
    //})
    //.directive('field', function () {
    //    return {
    //        require: ['ngModel', '?^quickInput', '?^form'],
    //        link: function (scope, element, attrs, ctrls) {
    //            var ngModelCtrl = ctrls[0],
    //                quickInputCtrl = ctrls[1],
    //                ngFormCtrl = ctrls[2];
    //            // Give the input its name
    //            ngModelCtrl.$name = attrs.id;
    //            // Tell the form all about it
    //            if (ngFormCtrl) {
    //                ngFormCtrl.$addControl(ngModelCtrl);
    //            }
    //            if (quickInputCtrl) {
    //                quickInputCtrl.setLabel(attrs.title);
    //                quickInputCtrl.setId(attrs.id);
    //                quickInputCtrl.setModelCtrl(ngModelCtrl);
    //            }
    //        }
    //    };
    //})
    //.directive('format', function ($filter, formatFactory) {
    //    return {
    //        scope: true,
    //        restrict: 'A',
    //        require: ['ngModel', '?^quickInput'],
    //        link: function (scope, element, attrs, ctrls) {
    //            var ngModelCtrl = ctrls[0],
    //                quickInputCtrl = ctrls[1],
    //                thisFormat = formatFactory[attrs.format];

    //            // This is the toModel routine
    //            var parser = function (value) {
    //                var removeParens;
    //                if (!value) {
    //                    return undefined;
    //                }
    //                // get rid of currency indicators
    //                value = value.toString().replace(thisFormat.replace, '');
    //                // Check for parens, currency filter (5) is -5
    //                removeParens = value.replace(/[\(\)]/g, '');
    //                // having parens indicates the number is negative
    //                if (value.length !== removeParens.length) {
    //                    value = -removeParens;
    //                }
    //                return value || undefined;
    //            },
    //            // This is the toView routine
    //            formatter = function (value) {
    //                // the currency filter returns undefined if parse error
    //                return $filter(attrs.format)(parser(value)) || thisFormat.symbol || '';
    //            };

    //            // This sets the format/parse to happen on blur/focus
    //            element.on("blur", function () {
    //                ngModelCtrl.$setViewValue(formatter(this.value));
    //                ngModelCtrl.$render();
    //            }).on("focus", function () {
    //                ngModelCtrl.$setViewValue(parser(this.value));
    //                ngModelCtrl.$render();
    //            });

    //            // Model Formatter
    //            ngModelCtrl.$formatters.push(formatter);
    //            // Model Parser
    //            ngModelCtrl.$parsers.push(parser);

    //            if (quickInputCtrl) {
    //                quickInputCtrl.setFormatting(thisFormat);
    //            }
    //        }
    //    }
    //});// JavaScript source code

    }]);

})();



