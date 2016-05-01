(function () {
    'use strict';

    angular
        .module('app')
        .factory('servicesFactory', ['$http', function ($http) {

            var servicesFactory = {};

            var urlBase = '/api';
            var invoiceAPI = '/InvoicesAPI';
            var invoiceDetailsAPI = '/InvoicesDetailsAPI';
            var customerAPI = '/CustomersAPI';
            var employeeAPI = '/EmployeesAPI';
            var productAPI = '/ProductsAPI';
            var unitTypeAPI = '/UnitTypesAPI';

            /* Invoices */
            servicesFactory.getInvoices = function () {
                return $http.get(urlBase + invoiceAPI);
            };
            servicesFactory.getInvoiceByID = function (id) {
                return $http.get(urlBase + invoiceAPI + '/' + id);
            };

            servicesFactory.insertInvoice = function (invoice) {
                return $http.post(urlBase, cust);
            };

            servicesFactory.updateInvoice = function (invoice) {
                return $http.put(urlBase + invoiceAPI + '/' + invoice.InvoiceID, invoice)
            };

            servicesFactory.deleteInvoiceByID = function (id) {
                return $http.delete(urlBase + invoiceAPI + '/' + id);
            };
            /* Customers */
            servicesFactory.getCustomers = function () {
                return $http.get(urlBase + customerAPI);
            };
            servicesFactory.getCustomerByID = function (id) {
                return $http.get(urlBase + customerAPI + '/' + id);
            };

            /* Employee */
            servicesFactory.getEmployees = function () {
                return $http.get(urlBase + employeeAPI);
            };
            servicesFactory.getEmployeeByID = function (id) {
                return $http.get(urlBase + employeeAPI + '/' + id);
            };

            /* Product */
            servicesFactory.getProducts = function () {
                return $http.get(urlBase + productAPI);
            };
            servicesFactory.getProductByID = function (id) {
                return $http.get(urlBase + productAPI + '/' + id);
            };

            /* UnitType */
            servicesFactory.getUnitTypes = function () {
                return $http.get(urlBase + unitTypeAPI);
            };
            servicesFactory.getUnitTypeByID = function (id) {
                return $http.get(urlBase + unitTypeAPI + '/' + id);
            };


            return servicesFactory;
        }

        ]);
})();