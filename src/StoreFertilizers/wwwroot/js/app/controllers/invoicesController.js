(function () {
    'use strict';

    angular
        .module('app')
        .controller('invoicesController', ['$scope', '$location', 'servicesFactory',
        function ($scope, $location, servicesFactory) {

            $scope.title = 'invoicesController';
            $scope.status = '';
            $scope.isEditMode = false;
            $scope.data = {
                customerList: null,
                employeeList: null,
                productList: null,
                unitTypeList: null,
                bankList: null,
                paymentTypeList: null
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
                invoiceDetails: [],
                netTotal: 0.00
            };

            $scope.getAllInvoices = function()  {
                servicesFactory.getInvoices()
                .then(function (response) {
                    $scope.invoices = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load invoice data: ' + error.message;
                });
            }

            $scope.getAllCustomer = function()  {
                servicesFactory.getCustomers()
                .then(function (response) {
                    $scope.data.customerList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load customer data: ' + error.message;
                });
            }

            $scope.getAllEmployee = function()  {
                servicesFactory.getEmployees()
                .then(function (response) {
                    $scope.data.employeeList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load employee data: ' + error.message;
                });
            }

            $scope.getAllProducts = function()  {
                servicesFactory.getProducts()
                .then(function (response) {
                    $scope.data.productList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load product data: ' + error.message;
                });
            }

            $scope.getAllUnitTypes = function()  {
                servicesFactory.getUnitTypes()
                .then(function (response) {
                    $scope.data.unitTypeList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load unit type data: ' + error.message;
                });
            }

            $scope.getAllBanks = function() {
                servicesFactory.getBanks()
                .then(function (response) {
                    $scope.data.bankList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load banks data: ' + error.message;
                });
            }

            $scope.getAllPaymentTypes = function () {
                servicesFactory.getPaymentTypes()
                .then(function (response) {
                    $scope.data.paymentTypeList = response.data;
                }, function (error) {
                    $scope.status = 'Unable to load payment type data: ' + error.message;
                });
            }

            $scope.insertInvoice = function () {
                servicesFactory.insertInvoice($scope.newInvoice)
                .then(function (response) {
                    $scope.status = response.data;
                }, function (error) {
                    $scope.status = 'Unable to create invoice data: ' + error.message;
                });
            }

            $scope.updateInvoice = function () {
                servicesFactory.updateInvoice($scope.newInvoice)
                .then(function (response) {
                    $scope.status = response.data;
                }, function (error) {
                    $scope.status = 'Unable to update invoice data: ' + error.message;
                });
            }
            
            // Adds an item to the invoice's items
            $scope.addItem = function () {
                var newInvoiceDetail = { no: $scope.newInvoice.invoiceDetails.length + 1, invoiceID: $scope.newInvoice.invoiceID, qty: 0, pricePerUnit: 0, discount: 0, amount: 0 };
                if ($scope.isEditMode) {
                    servicesFactory.insertInvoiceDetail($scope.newInvoice.invoiceDetails[$scope.newInvoice.invoiceDetails.length - 1])
                    .then(function (response) {
                        $scope.status = response.data;
                        $scope.newInvoice.invoiceDetails.push($scope.newInvoice.invoiceDetails[$scope.newInvoice.invoiceDetails.length - 1]);
                    }, function (error) {
                        $scope.status = 'Unable to delete invoice detail data: ' + error.message;
                    });
                } else {
                    $scope.newInvoice.invoiceDetails.push(newInvoiceDetail);
                }
            }

            // Remotes an item from the invoice
            $scope.removeInvoiceDetail = function (item) {
                if (confirm("โปรดยืนยันการลบ !") == true) {
                    if ($scope.isEditMode) {
                        servicesFactory.deleteInvoiceDetailByID(item.invoiceDetailsID)
                        .then(function (response) {
                            $scope.status = response.data;
                            $scope.newInvoice.invoiceDetails.splice($scope.newInvoice.invoiceDetails.indexOf(item), 1);
                        }, function (error) {
                            $scope.status = 'Unable to delete invoice detail data: ' + error.message;
                        });
                    } else {
                        $scope.newInvoice.invoiceDetails.splice($scope.newInvoice.invoiceDetails.indexOf(item), 1);
                    }
                    //re index
                    for (var i = 0; i < $scope.newInvoice.invoiceDetails.length; i++) {
                        $scope.newInvoice.invoiceDetails[i].no = i + 1;
                    }
                }
            };

            // Calculates the tax of the invoice
            $scope.calculateAmount = function (item) {
                item.amount = (item.pricePerUnit * item.qty) * (1 - item.discount / 100);
                return item.amount;
            };

            // Calculates the sub total of the invoice
            $scope.invoiceSubTotal = function () {
                var total = 0.00;
                angular.forEach($scope.newInvoice.invoiceDetails, function (item, key) {
                    total += (item.amount);
                });
                return total;
            };

            // Calculates the tax of the invoice
            $scope.calculateTax = function () {
                return ((7 * $scope.invoiceSubTotal()) / 100);
            };

            // Calculates the net total of the invoice
            $scope.calculateNetTotal = function () {
                //saveInvoice();
                $scope.newInvoice.netTotal = $scope.invoiceSubTotal();
                return $scope.newInvoice.netTotal;
            };

            //Init all data
            (function init() {
                //getAllInvoices();
                $scope.getAllCustomer();
                $scope.getAllEmployee();
                $scope.getAllProducts();
                $scope.getAllUnitTypes();
                $scope.getAllBanks();
                $scope.getAllPaymentTypes();

                var absUrl = $location.absUrl();
                if (absUrl.indexOf('/Create') >= 0)
                {
                    $scope.addItem();
                    $scope.isEditMode = false;
                } else if (absUrl.indexOf('/Edit/') >= 0)
                {
                    servicesFactory.getInvoiceByID('1')
                    .then(function (response) {
                        $scope.newInvoice = response.data;
                        $scope.newInvoice.invoiceDetails.push({invoiceID: $scope.newInvoice.invoiceID, pricePerUnit: 0, discount: 0, amount: 0 });
                        $scope.isEditMode = true;
                        //re index
                        for (var i = 0; i < $scope.newInvoice.invoiceDetails.length; i++) {
                            $scope.newInvoice.invoiceDetails[i].no = i + 1;
                        }
                    }, function (error) {
                        $scope.status = 'Unable to load invoice data: ' + error.message;
                    });
                }
                
            })()
            //End init
        }
        ]);
})();
