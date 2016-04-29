(function () {
    'use strict';

    angular
        .module('app')
        .controller('invoicesController', ['$scope', 'servicesFactory',
        function ($scope, servicesFactory) {

            $scope.title = 'invoicesController';
            $scope.status = '';
            $scope.invoices = {};

            getInvoices();

            function getInvoices() {
                servicesFactory.getInvoices()
                .then(function (response) {
                    $scope.invoices = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load invoice data: ' + error.message;
                });
            }

        }
        ]);
})();
