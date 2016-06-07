(function () {
    'use strict';

    angular
        .module('app')
        .controller('productsController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
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
                    pageSize: 50,
                    currentPage: 1
                }
            };

            $scope.products = {};

            // Get All
            $scope.getProductsByFilters = function () {
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

                servicesFactory.getProducts(params)
                .then(function (response) {
                    $scope.products = response.data.content;
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
            };

            //Init all data
            (function init() {
                $scope.getProductsByFilters();
            })()
            //End init
        }
        ]);
})();
