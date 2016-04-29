(function () {
    'use strict';

    angular
        .module('app')
        .factory('servicesFactory', ['$http', function ($http) {

            var servicesFactory = {};

            var urlBase = '/api';
            var invoiceAPI = '/invoicesAPI';


            servicesFactory.getInvoices = function () {
                return $http.get(urlBase + invoiceAPI);
            };

            return servicesFactory;
        }

        ]);
})();