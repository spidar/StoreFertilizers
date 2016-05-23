﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('purchasesController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            $scope.status = '';
            $scope.selected = {};
            $scope.isNewItem = false;
            $scope.showLoading = false;
            $scope.data = {
                providerList: null,
                productList: null,
                unitTypeList: null,

                totalPages: 0,
                totalItems: 0,
                filterOptions: {
                    filterText: '',
                    fromPurchaseDate: null,
                    toPurchaseDate: null,
                    externalFilter: 'searchText',
                    useExternalFilter: true
                },
                sortOptions: {
                    fields: ['purchaseID', 'purchaseDate', 'billNumber', 'productName', 'providerName'],
                    field: 'purchaseDate',
                    directions: ['desc', 'asc'],
                    sortReverse: true
                },
                pagingOptions: {
                    pageSizes: [20, 50, 100],
                    pageSize: 20,
                    currentPage: 1
                }
            };

            $scope.purchases = {};
            $scope.newPurchase = {};


            // Get All
            $scope.getPurchasesByFilters = function () {
                if ($scope.isNewItem) {
                    $scope.reset($scope.selected);
                }
                $scope.showLoading = true;

                var params = {
                    searchtext: $scope.data.filterOptions.filterText,
                    fromPurchaseDate: $scope.data.filterOptions.fromPurchaseDate,
                    toPurchaseDate: $scope.data.filterOptions.toPurchaseDate,
                    page: $scope.data.pagingOptions.currentPage,
                    pageSize: $scope.data.pagingOptions.pageSize,
                    sortBy: $scope.data.sortOptions.field,
                    sortDirection: ($scope.data.sortOptions.sortReverse) ? 'desc' : 'asc'
                };

                servicesFactory.getPurchases(params)
                .then(function (response) {
                    $scope.purchases = response.data.content;
                    $scope.data.totalItems = response.data.totalRecords;
                    $scope.data.totalPages = response.data.totalPages;
                    $scope.data.pagingOptions.currentPage = response.data.currentPage;

                    //if(!!$scope.purchases && $scope.purchases.length > 0)
                    //{
                        //for(var i = 0; i < $scope.purchases.length; i++)
                        //{
                            //$scope.purchases[i].purchaseDate = new Date($scope.purchases[i].purchaseDate);
                            //console.log('before : ' + $scope.purchases[i].purchaseDate);
                            //$scope.purchases[i].purchaseDate = moment($scope.purchases[i].purchaseDate).format('DD/MM/YYYY');
                            //console.log('after : ' + $scope.purchases[i].purchaseDate);
                        //}
                    //}
                    $timeout(function () {
                        $scope.showLoading = false;
                        $scope.status = '';
                    }, 1000);
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลการซื้อสินค้าได้ : ' + error.statusText;
                    $timeout(function () {
                        $scope.showLoading = false;
                    }, 1000);
                });
            }
            $scope.getAllProducts = function () {
                servicesFactory.getProducts()
                .then(function (response) {
                    $scope.data.productList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลสินค้าได้: ' + error.statusText;
                });
            }
            $scope.getAllProviders = function () {
                servicesFactory.getProviders()
                .then(function (response) {
                    $scope.data.providerList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลผู้ให้บริการได้: ' + error.statusText;
                });
            }
            $scope.getAllUnitTypes = function () {
                servicesFactory.getUnitTypes()
                .then(function (response) {
                    $scope.data.unitTypeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลหน่วยได้: ' + error.statusText;
                });
            }
            //
            $scope.getTemplate = function (purchase) {
                if (purchase.purchaseID === $scope.selected.purchaseID)
                    return 'edit';
                else
                    return 'display';
            };
            $scope.reset = function (purchase) {
                $scope.status = '';
                $scope.selected = {};
                if ($scope.isNewItem) {
                    $scope.purchases.splice($scope.purchases.indexOf(purchase), 1);
                    $scope.isNewItem = false;
                }
            };
            $scope.editPurchase = function (purchase) {
                if ($scope.isNewItem) {
                    $scope.reset($scope.selected);
                }
                $scope.selected = angular.copy(purchase);
                for (var i = 0; i < $scope.data.unitTypeList.length; i++) {
                    if ($scope.selected.product.unitTypeID == $scope.data.unitTypeList[i].unitTypeID) {
                        $scope.selected.product.unitType = angular.copy($scope.data.unitTypeList[i]);
                    }
                }
            };
            // Adds an item to the purchases
            $scope.addItem = function () {
                if ($scope.isNewItem)
                {
                    return false;
                }
                $scope.selected = {
                    amount: 0,
                    billNumber: '',
                    isTax: false,
                    notes: null,
                    product: null,
                    productID: 0,
                    provider: null,
                    providerID: 0,
                    providerName: '',
                    purchaseDate: moment().format('YYYY-MM-DDThh:mm:ss'),
                    purchaseNumber: '',
                    purchasePricePerUnit: 0,
                    qty: 0, 
                    qtyRemain: 0,
                    salePrice: 0,
                    unitType: null,
                    unitTypeID: 0,
                    vat: 0
                };
                $scope.purchases.unshift($scope.selected);
                $scope.isNewItem = true;
            }

            $scope.updatePurchase = function (idx) {
                if ($scope.myForm.$invalid)
                {
                    /*
                    if (!!$scope.myForm.$error)
                    {
                        if (!!$scope.myForm.$error.$required)
                        {
                            for(var i = 0; i < $scope.myForm.$error.$required)
                            alert('required');
                        }
                    }
                    */
                    $scope.status = 'กรุณากรอกข้อมูลให้ถูกต้องและครบถ้วน';
                    return false;
                }
                $scope.status = '';
                $scope.showLoading = true;
                if ($scope.isNewItem) {
                    servicesFactory.insertPurchase($scope.selected)
                    .then(function (response) {
                        $scope.selected.purchaseID = response.data.purchaseID;
                        $scope.purchases[idx] = angular.copy($scope.selected);
                        $scope.isNewItem = false;
                        $scope.reset();
                        $timeout(function () {
                            $scope.showLoading = false;
                            $scope.status = '';
                        }, 1000);
                    }, function (error) {
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.statusText;
                        $timeout(function () {
                            $scope.showLoading = false;
                        }, 1000);
                    });
                } else {
                    servicesFactory.updatePurchase($scope.selected)
                    .then(function (response) {
                        $scope.purchases[idx] = angular.copy($scope.selected);
                        $scope.reset();
                        $timeout(function () {
                            $scope.showLoading = false;
                            $scope.status = '';
                        }, 1000);
                    }, function (error) {
                        $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.statusText;
                        $timeout(function () {
                            $scope.showLoading = false;
                        }, 1000);
                    });
                }
            }
            // Remove an item from the purchase
            $scope.removePurchase = function (item) {
                if (confirm("โปรดยืนยันการลบ !") == true) {
                    $scope.showLoading = true;
                    servicesFactory.deletePurchaseByID(item.purchaseID)
                    .then(function (response) {
                        $scope.status = '';
                        $scope.reset();
                        $scope.purchases.splice($scope.purchases.indexOf(item), 1);
                        $timeout(function () {
                            $scope.showLoading = false;
                            $scope.status = '';
                        }, 1000);
                    }, function (error) {
                        $scope.status = 'ไม่สามารถลบข้อมูลได้ : ' + error.statusText;
                        $timeout(function () {
                            $scope.showLoading = false;
                        }, 1000);
                    });
                }
            };


            //Init all data
            (function init() {
                $scope.getPurchasesByFilters();
                $scope.getAllProviders();
                $scope.getAllProducts();
                $scope.getAllUnitTypes();

            })()
            //End init
        }
        ]);
})();
