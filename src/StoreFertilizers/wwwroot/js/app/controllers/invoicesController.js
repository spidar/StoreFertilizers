(function () {
    'use strict';

    angular
        .module('app')
        .controller('invoicesController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

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
                    $scope.status = 'ไม่สามารถโหลดข้อมูลใบส่งสินค้าได้: ' + error.message;
                });
            }

            $scope.getAllCustomer = function()  {
                servicesFactory.getCustomers()
                .then(function (response) {
                    $scope.data.customerList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลลูกค้าได้: ' + error.message;
                });
            }

            $scope.getAllEmployee = function()  {
                servicesFactory.getEmployees()
                .then(function (response) {
                    $scope.data.employeeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลลูกจ้างได้: ' + error.message;
                });
            }

            $scope.getAllProducts = function()  {
                servicesFactory.getProducts()
                .then(function (response) {
                    $scope.data.productList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลสินค้าได้: ' + error.message;
                });
            }

            $scope.getAllUnitTypes = function()  {
                servicesFactory.getUnitTypes()
                .then(function (response) {
                    $scope.data.unitTypeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลหน่วยได้: ' + error.message;
                });
            }

            $scope.getAllBanks = function() {
                servicesFactory.getBanks()
                .then(function (response) {
                    $scope.data.bankList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลธนาคารได้: ' + error.message;
                });
            }

            $scope.getAllPaymentTypes = function () {
                servicesFactory.getPaymentTypes()
                .then(function (response) {
                    $scope.data.paymentTypeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลชนิดของการจ่ายเงินได้: ' + error.message;
                });
            }

            $scope.saveInvoice = function () {
                var absUrl = $location.absUrl();
                if (absUrl.indexOf('/Create') >= 0)
                {
                    servicesFactory.insertInvoice($scope.newInvoice)
                    .then(function (response) {
                        $scope.status = '...บันทีกเรียบร้อย !';
                        $timeout(function () {
                            $scope.status = '';
                        }, 5000);
                    }, function (error) {
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.message;
                    });
                } else if (absUrl.indexOf('/Edit/') >= 0)
                {
                    servicesFactory.updateInvoice($scope.newInvoice)
                    .then(function (response) {
                        $scope.status = '...บันทีกเรียบร้อย !';
                        $timeout(function() {
                            $scope.status = '';
                        }, 5000); 
                    }, function (error) {
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.message;
                    });
                }
            }
            
            $scope.reindexItem = function () {
                //re-index
                var counter = 1;
                for (var i = 0; i < $scope.newInvoice.invoiceDetails.length; i++) {
                    if ($scope.newInvoice.invoiceDetails[i].isDeleted != true) {
                        $scope.newInvoice.invoiceDetails[i].no = counter;
                        counter = counter + 1;
                    }
                }
            }
            // Adds an item to the invoice's items
            $scope.addItem = function () {                
                var newInvoiceDetail = {no: 0, invoiceID: $scope.newInvoice.invoiceID, qty: 0, pricePerUnit: 0, discount: 0, amount: 0 };
                $scope.newInvoice.invoiceDetails.push(newInvoiceDetail);
                $scope.reindexItem();                
            }

            // Remotes an item from the invoice
            $scope.removeInvoiceDetail = function (item) {
                if (confirm("โปรดยืนยันการลบ !") == true) {
                    if (item.invoiceDetailsID == 0) {
                        $scope.newInvoice.invoiceDetails.splice($scope.newInvoice.invoiceDetails.indexOf(item), 1);
                    } else {
                        item.isDeleted = true;
                    }
                    $scope.reindexItem();
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
                var id = absUrl.substr(absUrl.lastIndexOf('/') + 1);
                if (absUrl.indexOf('/Create') >= 0)
                {
                    $scope.addItem();
                    $scope.isEditMode = false;
                } else if (absUrl.indexOf('/Edit/') >= 0)
                {
                    servicesFactory.getInvoiceByID(id)
                    .then(function (response) {
                        $scope.newInvoice = response.data;
                        $scope.isEditMode = true;
                        $scope.reindexItem();
                        $scope.newInvoice.deliveryDate = new Date($scope.newInvoice.deliveryDate);
                    }, function (error) {
                        $scope.status = 'ไม่สามารถโหลดข้อมูลใบส่งสินค้าได้ ' + error.message;
                    });
                }
                
            })()
            //End init
        }
        ]);
})();


angular.module('app').directive('datepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(function () {
                //var modelAccessor = $parse(attrs.ngModel);

                element.datepicker({
                    //language: 'th-th',
                    dateFormat: 'dd/mm/yyyy',
                    //setDate: null,
                    //autoclose: true,
                    onSelect: function (date) {
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    }
                });

                scope.$watch(attrs.ngModel, function (val) {
                    if (!val) {
                        return false;
                    }
                    //console.log(val);
                    var date = new Date(val);
                    //element.datepicker("setDate", val);
                });
            });
        }
    }
});
