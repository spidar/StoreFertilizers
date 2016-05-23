(function () {
    'use strict';

    angular
        .module('app')
        .factory('servicesFactory', ['$http', function ($http) {

            var servicesFactory = {};

            var urlBase = '/api';
            var invoiceAPI = '/InvoicesAPI';
            var invoiceDetailsAPI = '/InvoiceDetailsAPI';
            var purchasesAPI = '/PurchasesAPI';
            var providersAPI = '/ProvidersAPI'
            var customerAPI = '/CustomersAPI';
            var employeeAPI = '/EmployeesAPI';
            var productAPI = '/ProductsAPI';
            var unitTypeAPI = '/UnitTypesAPI';
            var bankAPI = '/BanksAPI';
            var paymentTypesAPI = '/PaymentTypesAPI';

            /* Invoices */
            servicesFactory.getInvoices = function () {
                return $http.get(urlBase + invoiceAPI);
            };
            servicesFactory.getInvoiceByID = function (id) {
                return $http.get(urlBase + invoiceAPI + '/' + id);
            };

            servicesFactory.insertInvoice = function (invoice) {
                return $http.post(urlBase + invoiceAPI, invoice);
            };

            servicesFactory.updateInvoice = function (invoice) {
                return $http.put(urlBase + invoiceAPI + '/' + invoice.invoiceID, invoice)
            };

            servicesFactory.deleteInvoiceByID = function (id) {
                return $http.delete(urlBase + invoiceAPI + '/' + id);
            };

            /* InvoiceDetails */
            servicesFactory.getInvoiceDetails = function () {
                return $http.get(urlBase + invoiceDetailsAPI);
            };
            servicesFactory.getInvoiceDetailByID = function (id) {
                return $http.get(urlBase + invoiceDetailsAPI + '/' + id);
            };
            servicesFactory.insertInvoiceDetail = function (invoicedetail) {
                return $http.post(urlBase + invoiceDetailsAPI, invoicedetail);
            };

            servicesFactory.updateInvoiceDetail = function (invoicedetail) {
                return $http.put(urlBase + invoiceDetailsAPI + '/' + invoicedetail.invoiceDetailsID, invoicedetail)
            };

            servicesFactory.deleteInvoiceDetailByID = function (id) {
                return $http.delete(urlBase + invoiceDetailsAPI + '/' + id);
            };

            /* Purchases */
            servicesFactory.getPurchases = function (parameters) {
                return $http.get(urlBase + purchasesAPI, {
                    params: parameters
                });
            };
            servicesFactory.getPurchaseTax = function () {
                return $http.get(urlBase + purchasesAPI + '/' + 'GetPurchaseTax');
            };
            servicesFactory.getPurchaseByID = function (id) {
                return $http.get(urlBase + purchasesAPI + '/' + id);
            };

            servicesFactory.insertPurchase = function (purchase) {
                return $http.post(urlBase + purchasesAPI, purchase);
            };

            servicesFactory.updatePurchase = function (purchase) {
                return $http.put(urlBase + purchasesAPI + '/' + purchase.purchaseID, purchase)
            };

            servicesFactory.deletePurchaseByID = function (id) {
                return $http.delete(urlBase + purchasesAPI + '/' + id);
            };

            /* Customers */
            servicesFactory.getProviders = function () {
                return $http.get(urlBase + providersAPI);
            };
            servicesFactory.getProviderByID = function (id) {
                return $http.get(urlBase + providersAPI + '/' + id);
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

            /* Bank */
            servicesFactory.getBanks = function () {
                return $http.get(urlBase + bankAPI);
            };
            servicesFactory.getBankByID = function (id) {
                return $http.get(urlBase + bankAPI + '/' + id);
            };

            /* PaymentType */
            servicesFactory.getPaymentTypes = function () {
                return $http.get(urlBase + paymentTypesAPI);
            };
            servicesFactory.getPaymentTypByID = function (id) {
                return $http.get(urlBase + paymentTypesAPI + '/' + id);
            };

            return servicesFactory;
        }

        ]);
})();