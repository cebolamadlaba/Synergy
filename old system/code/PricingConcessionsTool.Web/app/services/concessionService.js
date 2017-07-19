(function () {
    var app = angular.module('myApp');

    app.factory('concessionService', ['$http', 'AppSettings', concessionService]);

    function concessionService($http, AppSettings) {
        var service = {};

        service.getConcessions = function (aNumber, roleId, pending) {
            return $http({
                method: 'GET',
                url: AppSettings.apiServiceBaseUri + 'Concession/GetConcessions',
                params:
                    {
                        username: aNumber,
                        roleId: roleId,
                        pending: pending
                    }
            })

        }

        service.getRiskGroup = function (riskGroupNumber) {
            return $http({
                method: 'GET',
                url: AppSettings.apiServiceBaseUri + 'Concession/getRiskGroup',
                params:
                    {
                        riskGroupNumber: riskGroupNumber
                    }
            })

        }


        service.getConcession = function (concessionId) {
            return $http({
                method: 'GET',
                url: AppSettings.apiServiceBaseUri + 'Concession/getConcession',
                params:
                    {
                        concessionId: concessionId
                    }
            })

        }

        service.generateLetters = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Concession/GenerateLetters',
                data: data,
                cache: false,
                responseType: 'arraybuffer'
            })

        }
        service.getFinancialInfo = function (customerId, product) {
            return $http({
                method: 'GET',
                url: AppSettings.apiServiceBaseUri + product + '/getFinancialInfo',
                params:
                    {
                        customerId: customerId
                    }
            })

        }
        service.GetCustomerAccounts = function (product, riskGroupid, productTypeId) {
            return $http({
                method: 'GET',
                url: AppSettings.apiServiceBaseUri + product + '/GetCustomerAccounts',
                params:
                    {
                        riskGroupid: riskGroupid,
                        productTypeId: productTypeId
                    }
            })

        }




        service.getCustomerConcessions = function (customerId, concessionType, pending)
        {
            return $http({
                method: 'GET',
                url: AppSettings.apiServiceBaseUri + 'Concession/GetCustomerConcessions',
                params:
                   {
                       customerId: customerId,
                       pending: pending,
                       concessionType: concessionType
                   }
            })
        }

        service.decline = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Concession/Decline',
                data: data
            })
        }
        service.approve = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Concession/Approve',
                data: data
            })
        }

        service.approveWithChanges = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + data.ConcessionTypeCode + '/ApproveWithChanges',
                data: data
            })
        }

        service.forward = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Concession/Forward',
                data: data
            })
        }

        service.acceptChanges = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Concession/AcceptChanges',
                data: data
            })
        }


        service.declineChanges = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Concession/DeclineChanges',
                data: data
            })
        }


        service.LendingSave = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'lending/Save',
                data: data
            })
        }

        service.InvestmentSave = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Investment/Save',
                data: data
            })
        }

        service.MasSave = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Mas/Save',
                data: data
            })
        }

        service.TradeSave = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Trade/Save',
                data: data
            })
        }

        service.BolSave = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Bol/Save',
                data: data
            })
        }

        service.BolEdit = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Bol/Edit',
                data: data
            })
        }

        service.TransactionalSave = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Transactional/Save',
                data: data
            })
        }

        service.CashSave = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + 'Cash/Save',
                data: data
            })
        }

        service.removeConcession = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + data.ConcessionTypeCode+ '/RemoveConcession',
                data: data
            })
        }

        service.EditConcession = function (data) {
            return $http({
                method: 'POST',
                url: AppSettings.apiServiceBaseUri + data.ConcessionTypeCode + '/Edit',
                data: data
            })
        }

        return service;
    };
})();

