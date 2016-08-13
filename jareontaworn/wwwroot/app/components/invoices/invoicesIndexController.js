(function () {
    'use strict';

    angular
        .module('app')
        .controller('invoicesIndexController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            var today = new Date();
            var val = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
            var offset = moment(val, 'DD/MM/YYYY').utcOffset();
            var dateOffset = new Date(moment(val, 'DD/MM/YYYY').add(offset, 'm'));
            var totalDays = moment(val, 'DD/MM/YYYY').add(offset, 'm');

            $scope.status = '';
            $scope.showLoading = false;
            $scope.data = {
                totalNetAmount: 0,
                totalPages: 0,
                totalItems: 0,
                filterOptions: {
                    filterText: '',
                    isTax: 'notax',
                    fromCreatedDate: new Date(totalDays.subtract(3, 'day')),
                    toCreatedDate: dateOffset,
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
                    pageSize: 50,
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
            $scope.removeInvoice = function (item) {
                if (confirm("โปรดยืนยันการลบ !") == true) {
                    $scope.showLoading = true;
                    servicesFactory.deleteInvoiceByID(item.invoiceID)
                    .then(function (response) {
                        $scope.status = 'ลบเรียบร้อย';
                        $scope.invoices.splice($scope.invoices.indexOf(item), 1);
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
            $scope.getInvoicesByFilters();
            //End init
        }
        ]);
})();
