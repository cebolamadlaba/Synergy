
(function () {
    'use strict';
    var app = angular.module('myApp');

    app.controller('mypricingController', ['$q', '$scope', '$rootScope', '$window', '$state', 'toastr', 'sharedDataService', 'concessionService', 'referenceService', mypricingController]);

    mypricingController.$inject = ['$q', '$scope', '$rootScope', '$window', '$state', 'toastr', 'sharedDataService', 'concessionService', 'referenceService'];

    function mypricingController($q, $scope, $rootScope, $window, $state, toastr, sharedDataService, concessionService, referenceService) {

        $rootScope.Title = "My pricing"     

        $scope.Customer = {}

        $scope.CustomerType = null;

        $scope.EntityList = []

        $scope.MarketSegmentList = []


        /*$scope.multiSelectSettings =
            {
            enableSearch: true,
            displayProp: 'CustomerName',
            scrollableHeight: '200px',
            scrollable: true,
            buttonClasses: 'btn btn-entity-select',
            smartButtonMaxItems: 10,
            dynamicTitle: true,
            externalIdProp: '',
            events: {
                onItemSelect: function (item) {
                    console.log('selected: ' + item);
                }
            }
        };*/

        $scope.init = function ()
        {
           // $scope.EntityList.push({ CustomerName: "A" }, { CustomerName: "B" }, { CustomerName: "C" })

            $scope.OnCustomerType(2)
        

            $rootScope.user = sharedDataService.getUser();

            $q.all(
            [
                referenceService.getReference('GetMarketSegments')
            ])
            .then(function success(collection)
            {
                angular.copy(collection[0].data, $scope.MarketSegmentList);
            })
        }

        $scope.OnProductSelection = function (product)
        {
            $scope.Customer.IsNewCustomer = true;

            var url = null;

            sharedDataService.Customer = $scope.Customer;

            sharedDataService.Concession = $scope.Concession;

            switch (product) {

                case "lending":

                    if ($scope.Customer.IsNewCustomer)
                    {
                        $state.go('lendingNew')
                    }
                    else 
                        {
                        $state.go('lendingView', { CustomerId: $scope.Customer.Entity.CustomerId })
                    }

                    break;

                case "Investment":
                    if ($scope.Customer.IsNewCustomer) {
                        $state.go('InvestmentNew')
                    }
                    else {
                        $state.go('InvestmentView', { CustomerId: $scope.Customer.Entity.CustomerId })
                    }
                    break;

                case "Mas":
                    if ($scope.Customer.IsNewCustomer) {
                        $state.go('MasNew')
                    }
                    else {
                        $state.go('MasView', { CustomerId: $scope.Customer.Entity.CustomerId })
                    }
                    break;

                case "Bol":
                    if ($scope.Customer.IsNewCustomer) {
                        $state.go('BolNew')
                    }
                    else {
                        $state.go('BolView', { CustomerId: $scope.Customer.Entity.CustomerId })
                    }

                    break;
                case "Trade":
                    if ($scope.Customer.IsNewCustomer) {
                        $state.go('TradeNew')
                    }
                    else {
                        $state.go('TradeView', { CustomerId: $scope.Customer.Entity.CustomerId })
                    }

                    break;
                case "Transactional":
                    if ($scope.Customer.IsNewCustomer) {
                        $state.go('TransactionalNew')
                    }
                    else {
                        $state.go('TransactionalView', { CustomerId: $scope.Customer.Entity.CustomerId })
                    }
                    break;
                case "Cash":
                    if ($scope.Customer.IsNewCustomer) {
                        $state.go('CashNew')
                    }
                    else {
                        $state.go('CashView', { CustomerId: $scope.Customer.Entity.CustomerId })
                    }

                    break;
            }

        }


        $scope.OnCustomerType = function (customerTypeId) {

            $scope.CustomerType = customerTypeId;

            if (customerTypeId == 1) {
                $scope.Customer.IsNewCustomer = true;
                $scope.Customer.RiskgroupNumber = 0;
                $scope.Customer.RiskgroupName = null;
                $scope.Customer.LatestCreditRiskScore = 0;
                $scope.Customer.Entity.CustomerName = null;
                $scope.Customer.Entity.MarketSegment = null;
                $scope.Customer.Entity = null;
            }
            else {
                $scope.Customer.IsNewCustomer = false;
                $scope.Customer.RiskgroupNumber = null;
                $scope.Customer.Entity.CustomerName = null;
                $scope.Customer.Entity.MarketSegment = null;
                $scope.Customer.RiskgroupName = null;
            }
        }

        $scope.OnRiskGroupNumberChange = function () 
        {
            //$scope.EntityList.push({ CustomerName: "A" }, { CustomerName: "B" }, { CustomerName: "C" })

            if ($scope.Customer.RiskgroupNumber.length > 0)
            {
                var promise = concessionService.getRiskGroup($scope.Customer.RiskgroupNumber);

                promise.then(
                      function success(response) {
                          $scope.Customer.RiskgroupName = response.data.RiskGroupName;
                          ///$scope.EntityList = response.data.EntityList;

                          angular.forEach(response.data.EntityList, function (value, key)
                          {
                              $scope.EntityList.push(value);

                          })

                          ///$scope.$apply()

                      },
                      function error(response) {
                          alert(response.data.ExceptionMessage);
                      });
            }
        }
    }
})();
