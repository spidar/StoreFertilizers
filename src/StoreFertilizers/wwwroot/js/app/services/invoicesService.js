(function () {
    'use strict';

    var invoicesService = angular.module('invoicesService', ['ngResource']);

    invoicesService.factory('Invoices', ['$resource',
        function ($resource) {
            return $resource('/api/InvoicesAPI', {}, {
                query: {method: 'GET', params: {}, isArray: true}
            });
        }
    ]);
})();