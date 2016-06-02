(function () {
    'use strict';

    angular
        .module('app')
        .controller('stocksController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            $scope.status = '';
            $scope.selected = {};
            $scope.isNewItem = false;
            $scope.showLoading = false;
            $scope.data = {
                productList: null,
                productTypeList: null,
                unitTypeList: null,

                totalPages: 0,
                totalItems: 0,
                filterOptions: {
                    productType: null,
                    filterText: '',
                    externalFilter: 'searchText',
                    useExternalFilter: true
                },
                sortOptions: {
                    fields: ['stockID', 'productNumber', 'productName', 'location', 'notes'],
                    field: 'productName',
                    directions: ['desc', 'asc'],
                    sortReverse: false
                },
                pagingOptions: {
                    pageSizes: [20, 50, 100],
                    pageSize: 20,
                    currentPage: 1
                }
            };

            $scope.stocks = {};

            // Get All
            $scope.getStocksByFilters = function () {
                if ($scope.isNewItem) {
                    $scope.reset($scope.selected);
                }
                $scope.showLoading = true;

                var params = {
                    searchtext: $scope.data.filterOptions.filterText,
                    productTypeID: (!$scope.data.filterOptions.productType) ? 0 : $scope.data.filterOptions.productType.productTypeID,
                    page: $scope.data.pagingOptions.currentPage,
                    pageSize: $scope.data.pagingOptions.pageSize,
                    sortBy: $scope.data.sortOptions.field,
                    sortDirection: ($scope.data.sortOptions.sortReverse) ? 'desc' : 'asc'
                };

                servicesFactory.getStocks(params)
                .then(function (response) {
                    $scope.stocks = response.data.content;
                    $scope.data.totalItems = response.data.totalRecords;
                    $scope.data.totalPages = response.data.totalPages;
                    $scope.data.pagingOptions.currentPage = response.data.currentPage;
                    $timeout(function () {
                        $scope.showLoading = false;
                        $scope.status = '';
                    }, 1000);
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลสต๊อกได้ : ' + error.statusText;
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
            $scope.getAllProductTypes = function () {
                servicesFactory.getProductTypes()
                .then(function (response) {
                    $scope.data.productTypeList = response.data;
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลชนิดสินค้าได้: ' + error.statusText;
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
            $scope.getTemplate = function (stock) {
                if (stock.stockID === $scope.selected.stockID)
                    return 'edit';
                else
                    return 'display';
            };
            $scope.reset = function (stock) {
                $scope.status = '';
                $scope.selected = {};
                if ($scope.isNewItem) {
                    $scope.stocks.splice($scope.stocks.indexOf(stock), 1);
                    $scope.isNewItem = false;
                }
            };
            $scope.editStock = function (stock) {
                if ($scope.isNewItem) {
                    $scope.reset($scope.selected);
                }
                $scope.selected = angular.copy(stock);
                for (var i = 0; i < $scope.data.unitTypeList.length; i++) {
                    if ($scope.selected.product.unitTypeID == $scope.data.unitTypeList[i].unitTypeID) {
                        $scope.selected.product.unitType = angular.copy($scope.data.unitTypeList[i]);
                    }
                }
            };
            // Adds an item to the stocks
            $scope.addItem = function () {
                if ($scope.isNewItem)
                {
                    return false;
                }
                $scope.selected = {
                    product: null,
                    productID: 0,
                    balance: 0, 
                    alertLowStock: false,
                    notes: null
                };
                $scope.stocks.unshift($scope.selected);
                $scope.isNewItem = true;
            }

            $scope.updateStock = function (idx) {
                if ($scope.myForm.$invalid || $scope.selected.product == null)
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
                    servicesFactory.insertStock($scope.selected)
                    .then(function (response) {
                        $scope.stocks[idx] = angular.copy(response.data);
                        $scope.stocks[idx].productUnitTypeName = response.data.product.unitType.name;
                        $scope.isNewItem = false;
                        $scope.reset();
                        $timeout(function () {
                            $scope.showLoading = false;
                            $scope.status = '';
                        }, 1000);
                    }, function (error) {
                        if (error.status == 409) {
                            $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : มีสินค้านี้ในสต๊อกแล้ว';
                        } else {
                            $scope.status = 'ไม่สามารถบันทึกข้อมูลได้ : ' + error.statusText;
                        }
                        $timeout(function () {
                            $scope.showLoading = false;
                        }, 1000);
                    });
                } else {
                    servicesFactory.updateStock($scope.selected)
                    .then(function (response) {
                        $scope.stocks[idx] = angular.copy($scope.selected);
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
            // Remove an item from the stock
            $scope.removeStock = function (item) {
                if (confirm("โปรดยืนยันการลบ !") == true) {
                    $scope.showLoading = true;
                    servicesFactory.deleteStockByID(item.stockID)
                    .then(function (response) {
                        $scope.status = '';
                        $scope.reset();
                        $scope.stocks.splice($scope.stocks.indexOf(item), 1);
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

            //Init all data
            (function init() {
                $scope.getStocksByFilters();
                $scope.getAllProducts();
                $scope.getAllProductTypes();
                $scope.getAllUnitTypes();
            })()
            //End init
        }
        ]);
})();
