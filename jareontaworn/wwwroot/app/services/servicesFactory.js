﻿(function () {
    'use strict';

    angular
        .module('app')
        .factory('servicesFactory', ['$http', function ($http) {

            var servicesFactory = {};

            var urlBase = '/api';
            var invoiceAPI = '/InvoicesAPI';
            var invoiceAPI_CreateNewInvoice = '/CreateNewInvoice';
            var invoiceAPI_GetDashboardData = '/GetDashboardData';
            var invoiceDetailsAPI = '/InvoiceDetailsAPI';
            var purchasesAPI = '/PurchasesAPI';
            var purchasesAPI_GetPurchasesTax = '/GetPurchasesTax';
            var stocksAPI = '/StocksAPI';
            var providersAPI = '/ProvidersAPI'
            var getProvidersList = '/GetProvidersList';
            var customerAPI = '/CustomersAPI';
            var getCustomersList = '/GetCustomersList';
            var employeeAPI = '/EmployeesAPI';
            var productAPI = '/ProductsAPI';
            var productAPI_GetProductsList = '/GetProductsList';
            var productTypesAPI = '/ProductTypesAPI';
            var unitTypeAPI = '/UnitTypesAPI';
            var bankAPI = '/BanksAPI';
            var paymentTypesAPI = '/PaymentTypesAPI';

            /* Invoices */
            servicesFactory.getInvoices = function () {
                return $http.get(urlBase + invoiceAPI);
            };
            servicesFactory.getDashboardData = function () {
                return $http.get(urlBase + invoiceAPI + invoiceAPI_GetDashboardData);
            };
            servicesFactory.createNewInvoice = function (parameters) {
                return $http.get(urlBase + invoiceAPI + invoiceAPI_CreateNewInvoice, {
                    params: parameters
                });
            };
            servicesFactory.getInvoices = function (parameters) {
                return $http.get(urlBase + invoiceAPI, {
                    params: parameters
                });
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
            servicesFactory.getInvoiceDetails = function (parameters) {
                return $http.get(urlBase + invoiceDetailsAPI, {
                    params: parameters
                });
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
                return $http.get(urlBase + purchasesAPI + purchasesAPI_GetPurchasesTax);
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

            /* Stocks */
            servicesFactory.getStocks = function (parameters) {
                return $http.get(urlBase + stocksAPI, {
                    params: parameters
                });
            };
            servicesFactory.getStockByID = function (id) {
                return $http.get(urlBase + stocksAPI + '/' + id);
            };

            servicesFactory.insertStock = function (stock) {
                return $http.post(urlBase + stocksAPI, stock);
            };

            servicesFactory.updateStock = function (stock) {
                return $http.put(urlBase + stocksAPI + '/' + stock.stockID, stock)
            };

            servicesFactory.deleteStockByID = function (id) {
                return $http.delete(urlBase + stocksAPI + '/' + id);
            };

            /* Providers */
            servicesFactory.getProviders = function () {
                return $http.get(urlBase + providersAPI + getProvidersList);
            };
            servicesFactory.getProvidersByParams = function (parameters) {
                return $http.get(urlBase + providersAPI, {
                    params: parameters
                });
            };
            servicesFactory.getProviderByID = function (id) {
                return $http.get(urlBase + providersAPI + '/' + id);
            };

            /* Customers */
            servicesFactory.getCustomers = function () {
                return $http.get(urlBase + customerAPI + getCustomersList);
            };
            servicesFactory.getCustomersByParams = function (parameters) {
                return $http.get(urlBase + customerAPI, {
                    params: parameters
                });
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
            servicesFactory.getProductsList = function () {
                return $http.get(urlBase + productAPI + productAPI_GetProductsList);
            };
            servicesFactory.getProducts = function (parameters) {
                return $http.get(urlBase + productAPI, {
                    params: parameters
                });
            };
            servicesFactory.getProductByID = function (id) {
                return $http.get(urlBase + productAPI + '/' + id);
            }; 

            /* ProductType */
            servicesFactory.getProductTypes = function () {
                return $http.get(urlBase + productTypesAPI);
            };
            servicesFactory.getProductTypeByID = function (id) {
                return $http.get(urlBase + productTypesAPI + '/' + id);
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