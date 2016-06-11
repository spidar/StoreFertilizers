(function () {
    'use strict';

    angular
        .module('app')
        .controller('invoicesController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            var today = new Date();
            var val = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
            var offset = moment(val, 'DD/MM/YYYY').utcOffset();
            var dateOffset = new Date(moment(val, 'DD/MM/YYYY').add(offset, 'm'));
            var totalDays = moment(val, 'DD/MM/YYYY').add(offset, 'm');

            $scope.title = '';
            $scope.status = '';
            $scope.isEditMode = false;
            $scope.expandme = true;
            $scope.data = {
                printMode: true,
                printValueOnly: true,
                customerList: null,
                employeeList: null,
                productList: null,
                unitTypeList: null,
                //bankList: null,
                //paymentTypeList: null,
                purchaseList: null
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
                createdDate: dateOffset,
                dueDate: '',
                invoiceDetails: [],
                netTotal: 0.00,
                isTax: false
            };

            $scope.getAllCustomer = function()  {
                servicesFactory.getCustomers()
                .then(function (response) {
                    $scope.data.customerList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลลูกค้าได้: ' + error.statusText;
                });
            }

            $scope.getAllEmployee = function()  {
                servicesFactory.getEmployees()
                .then(function (response) {
                    $scope.data.employeeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลลูกจ้างได้: ' + error.statusText;
                });
            }

            $scope.getAllProducts = function()  {
                servicesFactory.getProductsList()
                .then(function (response) {
                    $scope.data.productList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลสินค้าได้: ' + error.statusText;
                });
            }

            $scope.getAllUnitTypes = function()  {
                servicesFactory.getUnitTypes()
                .then(function (response) {
                    $scope.data.unitTypeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลหน่วยได้: ' + error.statusText;
                });
            }

            $scope.getAllBanks = function() {
                servicesFactory.getBanks()
                .then(function (response) {
                    $scope.data.bankList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลธนาคารได้: ' + error.statusText;
                });
            }

            $scope.getAllPaymentTypes = function () {
                servicesFactory.getPaymentTypes()
                .then(function (response) {
                    $scope.data.paymentTypeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลชนิดของการจ่ายเงินได้: ' + error.statusText;
                });
            }

            $scope.getAllPurchases = function () {
                servicesFactory.getPurchaseTax()
                .then(function (response) {
                    $scope.data.purchaseList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลการซื้อสินค้าได้: ' + error.statusText;
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
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.statusText;
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
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.statusText;
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
                var newInvoiceDetail = { no: 0, invoiceDetailsID: 0, invoiceID: $scope.newInvoice.invoiceID, qty: 0, pricePerUnit: 0, discount: 0, amount: 0 };
                $scope.newInvoice.invoiceDetails.push(newInvoiceDetail);
                $scope.reindexItem();                
            }

            // Remotes an item from the invoice
            $scope.removeInvoiceDetail = function (item) {
                if (confirm("โปรดยืนยันการลบ !") == true) {
                    //Handle Tax case
                    if ($scope.newInvoice.isTax) {
                        var elementPos = $scope.data.purchaseList.map(function (x) { return x.purchaseID; }).indexOf(item.purchaseID);
                        if (elementPos !== -1)
                        {
                            var objectFound = $scope.data.purchaseList[elementPos];
                            objectFound.checked = false;
                        }
                    }
                    ////////////
                    if (item.invoiceDetailsID == 0) {
                        $scope.newInvoice.invoiceDetails.splice($scope.newInvoice.invoiceDetails.indexOf(item), 1);
                    } else {
                        item.isDeleted = true;
                    }
                    $scope.reindexItem();                    
                }
            };

            $scope.moneyToWord = function (money) {
                var result = '';
                var minus = '';

                if (money < 0) {
                    minus = 'ติดลบ';
                    money = money * -1;
                }

                money = parseFloat(Math.round(money * 100) / 100).toFixed(2);

                if (money == '0.00') {
                    result = 'ศูนย์บาทถ้วน';
                    return result;
                }

                var numbers = ['', 'หนึ่ง', 'สอง', 'สาม', 'สี่', 'ห้า', 'หก', 'เจ็ด', 'แปด', 'เก้า'];
                var positions = ['', 'สิบ', 'ร้อย', 'พัน', 'หมื่น', 'แสน'];

                var digit = money.length;
                var inputs = [];

                if (digit <= 15) {
                    if (digit > 9) {
                        inputs[0] = money.substr(0, digit - 9);
                        inputs[1] = money.substr(inputs[0].length, 6);
                    } else {
                        inputs[0] = '00';
                        inputs[1] = money.substr(0, money.length - 3);
                    }
                    inputs[2] = money.substr(money.indexOf('.') + 1, 2);
                } else {
                    result = 'Error: ไม่สามารถรองรับจำนวนเงินที่เกินหลักแสนล้าน';
                    return result;
                }

                for (var i = 0; i < 3; i++) {
                    var input = inputs[i];

                    if (input != '0' && input != '00') {
                        var digit = input.length;

                        for (var j = 0; j < digit; j++) {
                            var s = input.substr(j, 1);
                            var number = numbers[s];
                            var position = '';

                            if (number != '') {
                                position = positions[digit - (j + 1)];
                            }

                            if ((digit - j) == 2) {
                                if (s == '1') {
                                    number = '';
                                } else if (s == '2') {
                                    number = 'ยี่';
                                }
                            } else if ((digit - j) == 1 && (digit != 1)) {
                                var pre_s = '0';
                                if (j > 0) {
                                    pre_s = input.substr(j - 1, 1);
                                }

                                if (i == 0) {
                                    if (pre_s != '0') {
                                        if (s == '1') {
                                            number = 'เอ็ด';
                                        }
                                    }
                                } else {
                                    if (s == '1') {
                                        number = 'เอ็ด';
                                    }
                                }
                            }

                            result = result + number + position;
                        }
                    }

                    if (i == 0) {
                        if (input != '00') {
                            result = result + 'ล้าน';
                        }
                    } else if (i == 1) {
                        if (input != '0' && input != '00') {
                            result = result + 'บาท';
                            if (inputs[2] == '00') {
                                result = result + 'ถ้วน';
                            }
                        }
                    } else {
                        if (input != '00') {
                            result = result + 'สตางค์';
                        }
                    }
                }

                return minus + result;
            }
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

            // Calculates the sub total of the invoice
            $scope.invoiceTotalDiscount = function () {
                var total = 0.00;
                angular.forEach($scope.newInvoice.invoiceDetails, function (item, key) {
                    total += (item.pricePerUnit * item.qty) * (item.discount / 100);
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
            $scope.calculateNetTotalText = function () {                
                $scope.newInvoice.netTotalText = $scope.moneyToWord($scope.newInvoice.netTotal);
                return $scope.newInvoice.netTotalText;
            };

            $scope.purchaseItemChanged = function ($event, item) {
                var checkbox = $event.target;
                var action = (checkbox.checked ? 'add' : 'remove');

                var elementPos = $scope.newInvoice.invoiceDetails.map(function (x) { return x.purchaseID; }).indexOf(item.purchaseID);
                //var objectFound = $scope.newInvoice.invoiceDetails[elementPos];

                if (action === 'add' && elementPos === -1) {
                    var newInvoiceDetail = {
                        invoiceDetailsID: 0,
                        purchaseID: item.purchaseID,
                        no: 0,
                        productID: item.productID,
                        product: angular.copy(item.product),
                        unitTypeID: item.unitTypeID,
                        unitType: angular.copy(item.unitType),
                        invoiceID: $scope.newInvoice.invoiceID,
                        qty: item.qtyRemain,
                        pricePerUnit: 0,
                        discount: 0,
                        amount: 0
                    };
                    $scope.newInvoice.invoiceDetails.push(newInvoiceDetail);
                    $scope.reindexItem();
                }
                if (action === 'remove' && elementPos !== -1) {
                    $scope.newInvoice.invoiceDetails.splice($scope.newInvoice.invoiceDetails.indexOf($scope.newInvoice.invoiceDetails[elementPos]), 1);
                    $scope.reindexItem();
                }
            };

            /**
            * Create filter function for a query string
            */
            function createFilterFor(query) {
                var lowercaseQuery = angular.lowercase(query);
                return function filterFn(item) {
                    return (angular.lowercase(item.name).indexOf(lowercaseQuery) >= 0);
                };
            }

            $scope.getProductMatches = function (productSearchText) {
                var results = productSearchText ? $scope.data.productList.filter(createFilterFor(productSearchText)) : $scope.data.productList;
                return results;
            };

            $scope.getCustomerMatches = function (customerSearchText) {
                var results = customerSearchText ? $scope.data.customerList.filter(createFilterFor(customerSearchText)) : $scope.data.customerList;
                return results;
            };

            //Init all data
            (function init() {
                $scope.getAllCustomer();
                //$scope.getAllEmployee();
                $scope.getAllProducts();
                //$scope.getAllUnitTypes();
                //$scope.getAllBanks();
                //$scope.getAllPaymentTypes();
                $scope.getAllPurchases();

                var absUrl = $location.absUrl();
                var id = absUrl.substr(absUrl.lastIndexOf('/') + 1);
                if (absUrl.indexOf('/Create') >= 0)
                {
                    if (absUrl.indexOf('isTax=true') >= 0)
                    {
                        //$scope.newInvoice.isTax = true;
                    } else
                    {
                        $scope.addItem();
                    }
                    $scope.isEditMode = false;
                } else if (absUrl.indexOf('/Edit/') >= 0)
                {
                    servicesFactory.getInvoiceByID(id)
                    .then(function (response) {
                        $scope.newInvoice = response.data;
                        $scope.isEditMode = true;
                        $scope.reindexItem();
                    }, function (error) {
                        $scope.status = 'ไม่สามารถโหลดข้อมูลใบส่งสินค้าได้ ' + error.statusText;
                    });
                }
                
            })()
            //End init
        }
        ]);
})();