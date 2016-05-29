(function () {
    'use strict';

    angular
        .module('app')
        .controller('invoicesIndexController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            $scope.status = '';
            $scope.showLoading = false;
            $scope.data = {
                totalNetAmount: 0,
                totalPages: 0,
                totalItems: 0,
                filterOptions: {
                    filterText: '',
                    isTax: '',                    
                    fromCreatedDate: null,
                    toCreatedDate: null,
                    dueIn: '',
                    externalFilter: 'searchText',
                    useExternalFilter: true
                },
                sortOptions: {
                    fields: ['invoiceID', 'createdDate', 'invoiceNumber', 'cueDate', 'providerName'],
                    field: 'createdDate',
                    directions: ['desc', 'asc'],
                    sortReverse: true
                },
                pagingOptions: {
                    pageSizes: [20, 50, 100],
                    pageSize: 20,
                    currentPage: 1
                }
            };

            $scope.invoices = {};

            // Get All
            $scope.getInvoicesByFilters = function () {
                $scope.showLoading = true;

                var params = {
                    searchtext: $scope.data.filterOptions.filterText,
                    fromCreatedDate: $scope.data.filterOptions.fromCreatedDate,
                    toCreatedDate: $scope.data.filterOptions.toCreatedDate,
                    isTax: $scope.data.filterOptions.isTax,
                    dueIn: $scope.data.filterOptions.dueIn,
                    page: $scope.data.pagingOptions.currentPage,
                    pageSize: $scope.data.pagingOptions.pageSize,
                    sortBy: $scope.data.sortOptions.field,
                    sortDirection: ($scope.data.sortOptions.sortReverse) ? 'desc' : 'asc'
                };

                servicesFactory.getInvoices(params)
                .then(function (response) {
                    $scope.invoices = response.data.content;
                    $scope.data.totalNetAmount = response.data.totalNetAmount;
                    $scope.data.totalItems = response.data.totalRecords;
                    $scope.data.totalPages = response.data.totalPages;
                    $scope.data.pagingOptions.currentPage = response.data.currentPage;
                    $timeout(function () {
                        $scope.showLoading = false;
                        $scope.status = '';
                    }, 1000);
                }, function (error) {
                    $scope.status = 'ไม่สามารถโหลดข้อมูลได้ : ' + error.statusText;
                    $timeout(function () {
                        $scope.showLoading = false;
                    }, 1000);
                });
            }
            //
            // Remove an item from the stock
            $scope.removeInvoice = function (item) {
                if (confirm("โปรดยืนยันการลบ !") == true) {
                    $scope.showLoading = true;
                    servicesFactory.deleteInvoiceByID(item.invoiceID)
                    .then(function (response) {
                        $scope.status = '';
                        $scope.invoices.splice($scope.invoices.indexOf(item), 1);
                        $timeout(function () {
                            $scope.showLoading = false;
                            $scope.status = '';
                        }, 1000);
                    }, function (error) {
                        $scope.status = 'ไม่สามารถลบข้อมูลได้ : ' + error.statusText;
                        $scope.getInvoicesByFilters();
                        $timeout(function () {
                            $scope.showLoading = false;
                        }, 1000);
                    });
                }
            };

            //Init all data
            $scope.getInvoicesByFilters();
            //End init
        }
        ]);
})();
