(function () {
    'use strict';

    angular
        .module('app')
        .controller('invoicesController', ['$scope', 'servicesFactory',
        function ($scope, servicesFactory) {

            $scope.title = 'invoicesController';
            $scope.status = '';
            $scope.data = {
                customerList: null,
                employeeList: null,
                productList: null,
                unitTypeList: null
            };

            $scope.invoices = {};

            $scope.companyInfo = {
                name : 'หจก. เจริญถาวร',
                address : '94 ม.4 ต.โรงเข้ อ.บ้านแพ้ว จ.สมุทรสาคร 74120',
                phone : '(034) 467611-2',
                mobile : 'mobile number',
                fax : '(034) 4676113',
                taxernuber : '07'
            };

            $scope.newInvoice = {
                InvoiceDetails: []
            };

            function getAllInvoices() {
                servicesFactory.getInvoices()
                .then(function (response) {
                    $scope.invoices = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load invoice data: ' + error.message;
                });
            }

            function getAllCustomer() {
                servicesFactory.getCustomers()
                .then(function (response) {
                    $scope.data.customerList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load customer data: ' + error.message;
                });
            }

            function getAllEmployee() {
                servicesFactory.getEmployees()
                .then(function (response) {
                    $scope.data.employeeList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load employee data: ' + error.message;
                });
            }

            function getAllProducts() {
                servicesFactory.getProducts()
                .then(function (response) {
                    $scope.data.productList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load product data: ' + error.message;
                });
            }

            function getAllUnitTypes() {
                servicesFactory.getUnitTypes()
                .then(function (response) {
                    $scope.data.unitTypeList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load unit type data: ' + error.message;
                });
            }

            //Init all data
            (function init() {
                //getAllInvoices();
                getAllCustomer();
                getAllEmployee();
                getAllProducts();
                getAllUnitTypes();
            })()
            //End init

            // Adds an item to the invoice's items
            $scope.addItem = function () {
                $scope.newInvoice.InvoiceDetails.push({no: $scope.newInvoice.InvoiceDetails.length + 1, Qty: 0, PricePerUnit: 0, Discount: 0, Amount: 0 });
            }

            // Remotes an item from the invoice
            $scope.removeItem = function (item) {
                $scope.newInvoice.InvoiceDetails.splice($scope.newInvoice.InvoiceDetails.indexOf(item), 1);
                //re index
                for(var i = 0; i < $scope.newInvoice.InvoiceDetails.length; i++)
                {
                    $scope.newInvoice.InvoiceDetails[i].no = i + 1;
                }
            };
        }
        ]);
})();
