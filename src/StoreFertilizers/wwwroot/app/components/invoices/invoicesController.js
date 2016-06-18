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
            $scope.showLoading = false;
            $scope.isEditMode = false;
            $scope.expandme = true;
            $scope.data = {
                saved: false,
                printMode: false,
                printValueOnly: true,
                customerList: null,
                employeeList: null,
                productList: null,
                unitTypeList: null,
                //bankList: null,
                //paymentTypeList: null,
                purchaseList: [],
                calTax: false
            };

            $scope.invoices = {};
            $scope.blankInvoiceDetails = {};

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
                    for (var i = 0; i < response.data.length; i++) {
                        var foundPurchase = $scope.newInvoice.invoiceDetails.map(function (x) { return x.purchase.purchaseID; }).indexOf(response.data[i].purchaseID);
                        if (foundPurchase !== -1) {
                            response.data[i].checked = true;
                        }
                        $scope.data.purchaseList.push(response.data[i]);
                    }
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลการซื้อสินค้าได้: ' + error.statusText;
                });
            }

            $scope.insertInvoice = function () {
                servicesFactory.insertInvoice($scope.newInvoice)
                    .then(function (response) {
                        $scope.newInvoice = response.data;
                        $scope.reindexItem();
                        $scope.data.saved = true;
                        $scope.status = '...บันทีกเรียบร้อย !';
                        $scope.showLoading = false;
                        $timeout(function () {
                            $scope.status = '';
                        }, 5000);
                    }, function (error) {
                        $scope.showLoading = false;
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.statusText;
                    });
            }
            $scope.updateInvoice = function () {
                servicesFactory.updateInvoice($scope.newInvoice)
                    .then(function (response) {
                        $scope.newInvoice = response.data;
                        $scope.reindexItem();
                        $scope.status = '...บันทีกเรียบร้อย !';
                        $scope.showLoading = false;
                        $timeout(function () {
                            $scope.status = '';
                        }, 5000);
                    }, function (error) {
                        $scope.showLoading = false;
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.statusText;
                    });
            }

            $scope.saveInvoice = function () {
                if (!$scope.newInvoice.customer) {
                    $scope.status = 'กรุณาระบุลูกค้า';
                    return false;
                }
                if ($scope.newInvoice.invoiceDetails.length == 0) {
                    $scope.status = 'กรุณากรอกสินค้าอย่างน้อย 1 ชิ้น';
                    return false;
                }
                for (var i = 0; i < $scope.newInvoice.invoiceDetails.length; i++)
                {
                    if($scope.newInvoice.invoiceDetails[i].product == null)
                    {
                        $scope.status = 'กรุณากรอกสินค้าตามเครื่องหมาย ! ให้ถูกต้องและครบถ้วน';
                        return false;
                    }
                }
                var absUrl = $location.absUrl();
                $scope.showLoading = true;
                if (absUrl.indexOf('/Create') >= 0)
                {
                    if (!$scope.data.saved)
                    {
                        $scope.insertInvoice();
                    } else {
                        $scope.updateInvoice();
                    }
                    
                } else if (absUrl.indexOf('/Edit/') >= 0)
                {
                    $scope.updateInvoice();
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
                //for print
                $scope.fillBlankRows();
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
                            objectFound.qtyRemain = objectFound.qtyRemain + item.qty;
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
                $scope.newInvoice.subTotal = total;
                return total;
            };
            // Calculates the sub total of the invoice
            $scope.invoiceTotalDiscount = function () {
                var total = 0.00;
                angular.forEach($scope.newInvoice.invoiceDetails, function (item, key) {
                    total += (item.pricePerUnit * item.qty) * (item.discount / 100);
                });
                $scope.newInvoice.discount = total;
                return total;
            };
            // Calculates the tax of the invoice
            $scope.calculateTax = function () {
                if ($scope.data.calTax) {
                    $scope.newInvoice.vat = ((7 * $scope.invoiceSubTotal()) / 100);
                    return $scope.newInvoice.vat;
                }else
                {
                    $scope.newInvoice.vat = 0;
                    return 0;
                }
            };
            // Calculates the net total of the invoice
            $scope.calculateNetTotal = function () {
                //saveInvoice();
                $scope.newInvoice.netTotal = $scope.invoiceSubTotal();
                if (!!$scope.newInvoice.discount && $scope.newInvoice.discount <= $scope.newInvoice.netTotal) {
                    $scope.newInvoice.netTotal = $scope.newInvoice.netTotal - $scope.newInvoice.discount;
                }
                if ($scope.data.calTax) {
                    $scope.newInvoice.netTotal = $scope.newInvoice.netTotal + $scope.newInvoice.vat;
                }
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
                        purchase: item,
                        no: 0,
                        productID: item.productID,
                        product: angular.copy(item.product),
                        unitTypeID: item.unitTypeID,
                        unitType: angular.copy(item.unitType),
                        invoiceID: $scope.newInvoice.invoiceID,
                        //qty: item.qtyRemain,
                        qty: 0,
                        qtyRemain: item.qtyRemain,
                        pricePerUnit: 0,
                        discount: 0,
                        amount: 0
                    };
                    $scope.newInvoice.invoiceDetails.push(newInvoiceDetail);
                    $scope.reindexItem();
                } else if (action === 'add' && elementPos !== -1) {
                    var invoiceDetailItem = $scope.newInvoice.invoiceDetails[elementPos];
                    invoiceDetailItem.purchase = item;
                    invoiceDetailItem.isDeleted = false;
                    invoiceDetailItem.qtyRemain = item.qtyRemain;
                    invoiceDetailItem.qty = 0;
                }
                if (action === 'remove' && elementPos !== -1) {
                    var invoiceDetailItem = $scope.newInvoice.invoiceDetails[elementPos];
                    if (invoiceDetailItem.qtyRemain <= item.qty) {
                        item.qtyRemain = invoiceDetailItem.qtyRemain;
                    }
                    if (invoiceDetailItem.invoiceDetailsID !== 0) {
                        invoiceDetailItem.isDeleted = true;
                    } else {
                        $scope.newInvoice.invoiceDetails.splice($scope.newInvoice.invoiceDetails.indexOf($scope.newInvoice.invoiceDetails[elementPos]), 1);
                    }
                    $scope.reindexItem();
                }
            };

            $scope.modifyQtyRemain = function (item) {
                if ($scope.newInvoice.isTax) {
                    var elementPos = $scope.data.purchaseList.map(function (x) { return x.purchaseID; }).indexOf(item.purchaseID);
                    if (elementPos !== -1) {
                        var purchase = $scope.data.purchaseList[elementPos];
                        if (item.qty <= item.qtyRemain) {
                            purchase.qtyRemain = item.qtyRemain - item.qty;
                            //item.purchase = objectFound;
                        }else
                        {
                            item.qty = item.qtyRemain;
                            purchase.qtyRemain = 0;
                        }
                    }
                }
            };
            /**
            * Create filter function for a query string
            */
            $scope.createFilterFor = function(query) {
                var lowercaseQuery = angular.lowercase(query);
                return function filterFn(item) {
                    return (angular.lowercase(item.name).indexOf(lowercaseQuery) >= 0);
                };
            };

            $scope.getProductMatches = function (productSearchText) {
                var results = productSearchText ? $scope.data.productList.filter($scope.createFilterFor(productSearchText)) : $scope.data.productList;
                return results;
            };

            $scope.getCustomerMatches = function (customerSearchText) {
                var results = customerSearchText ? $scope.data.customerList.filter($scope.createFilterFor(customerSearchText)) : $scope.data.customerList;
                return results;
            };

            $scope.reloadPageData = function () {
                var absUrl = $location.absUrl();
                var id = absUrl.substr(absUrl.lastIndexOf('/') + 1);
                if (absUrl.indexOf('/Create') >= 0) {
                    var params = {
                        isTax: false
                    };
                    if (absUrl.indexOf('isTax=true') >= 0) {
                        params.isTax = true;
                        $scope.getAllPurchases();
                    }
                    servicesFactory.createNewInvoice(params)
                    .then(function (response) {
                        $scope.newInvoice = response.data;
                        $scope.reindexItem();
                    }, function (error) {
                        $scope.status = 'ไม่สามารถตั้งค่าเริ่มต้นได้ ' + error.statusText;
                    });
                    $scope.isEditMode = false;
                } else if (absUrl.indexOf('/Edit/') >= 0) {
                    
                    servicesFactory.getInvoiceByID(id)
                    .then(function (response) {
                        $scope.newInvoice = response.data;
                        
                        if (absUrl.indexOf('isTax=true') >= 0) {
                            $scope.getAllPurchases();
                            for (var i = $scope.newInvoice.invoiceDetails.length - 1; i >= 0 ; i--) {
                                var purchase = $scope.newInvoice.invoiceDetails[i].purchase;
                                $scope.newInvoice.invoiceDetails[i].qtyRemain = purchase.qtyRemain + $scope.newInvoice.invoiceDetails[i].qty;
                                purchase.checked = true;
                                if (purchase.qtyRemain == 0) {
                                    $scope.data.purchaseList.unshift(purchase);
                                }
                            }
                        }
                        $scope.data.calTax = ($scope.newInvoice.vat > 0);
                        $scope.isEditMode = true;
                        $scope.reindexItem();
                    }, function (error) {
                        $scope.status = 'ไม่สามารถโหลดข้อมูลใบส่งสินค้าได้ ' + error.statusText;
                    });
                }
            };
            $scope.fillBlankRows = function () {
                var blankRows = 11 - $scope.newInvoice.invoiceDetails.length;
                if (blankRows > 0) {
                    $scope.blankInvoiceDetails = new Array(blankRows);
                }else
                {
                    $scope.blankInvoiceDetails = 0;
                }
            };
            //Init all data
            (function init() {
                $scope.getAllCustomer();
                //$scope.getAllEmployee();
                $scope.getAllProducts();
                //$scope.getAllUnitTypes();
                //$scope.getAllBanks();
                //$scope.getAllPaymentTypes();
                $scope.reloadPageData();               
                
            })()
            //End init
        }
        ]);
})();