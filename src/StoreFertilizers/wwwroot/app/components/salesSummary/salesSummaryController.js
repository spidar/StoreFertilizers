(function () {
    'use strict';

    angular
        .module('app')
        .controller('salesSummaryController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            var today = new Date();
            var val = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
            var offset = moment(val, 'DD/MM/YYYY').utcOffset();
            var dateOffset = new Date(moment(val, 'DD/MM/YYYY').add(offset, 'm'));

            $scope.status = '';
            $scope.showLoading = false;
            $scope.data = {
                totalNetAmount: 0,
                totalPages: 0,
                totalItems: 0,
                totalNetAmountDetails: 0,
                totalPagesDetails: 0,
                totalItemsDetails: 0,
                totalProductDetails: 0,
                filterOptions: {
                    filterText: '',
                    filterTextDetails: '',
                    isTax: 'notax',
                    fromCreatedDate: dateOffset,
                    toCreatedDate: dateOffset,
                    fromCreatedDateDetails: dateOffset,
                    toCreatedDateDetails: dateOffset,
                    sumIn: 'today',
                    externalFilter: 'searchText',
                    useExternalFilter: true
                },
                sortOptions: {
                    fields: ['invoiceID', 'createdDate', 'invoiceNumber', 'cueDate', 'providerName'],
                    field: 'createdDate',
                    directions: ['desc', 'asc'],
                    fieldDetails: 'createdDate',
                    directionsDetails: ['desc', 'asc'],
                    sortReverse: true,
                    sortReverseDetails: true
                },
                pagingOptions: {
                    pageSizes: [20, 50, 100],
                    pageSize: 20,
                    currentPage: 1,
                    currentPageDetails: 1
                }
            };

            //Line Chart
            $scope.lineChartLabels = [];//["ม.ค.", "ก.พ.", "ม.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค."];
            $scope.lineChartSeries = ['ยอดขาย'];
            $scope.lineChartData = [[]];
            // End Line Chart

            $scope.invoices = {};
            $scope.invoiceDetails = {};

            $scope.sumInChange = function (item) {
                var today = new Date($scope.data.filterOptions.toCreatedDate);
                if (item == 'today')
                {
                    today = new Date();
                }
                var val = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
                var offset = moment(val, 'DD/MM/YYYY').utcOffset();
                var totalDays = moment(val, 'DD/MM/YYYY').add(offset, 'm');

                if (item == 'past7') {
                    totalDays = totalDays.subtract(7, 'day');
                } else if (item == 'past30') {
                    totalDays = totalDays.subtract(30, 'day');
                }

                $scope.data.filterOptions.fromCreatedDate = new Date(totalDays);
                $scope.data.filterOptions.toCreatedDate = new Date(moment(val, 'DD/MM/YYYY').add(offset, 'm'));
            }

            // Get All
            $scope.getInvoicesByFilters = function () {
                $scope.showLoading = true;

                var params = {
                    searchtext: $scope.data.filterOptions.filterText,
                    fromCreatedDate: $scope.data.filterOptions.fromCreatedDate,
                    toCreatedDate: $scope.data.filterOptions.toCreatedDate,
                    isTax: $scope.data.filterOptions.isTax,
                    sumIn: $scope.data.filterOptions.sumIn,
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
                    // Fill Chart
                    $scope.lineChartLabels = [];//["ม.ค.", "ก.พ.", "ม.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค."];
                    $scope.lineChartData = [[]];
                    for (var i = 0; i < $scope.invoices.length; i++) {
                        $scope.lineChartLabels.push(moment($scope.invoices[i].createdDate).format('DD/MM/YYYY'));
                        $scope.lineChartData[0].push($scope.invoices[i].netTotal);
                    }
                    //
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
            $scope.getInvoiceDetailsByFilters = function () {
                $scope.showLoading = true;

                var params = {
                    searchtext: $scope.data.filterOptions.filterText,
                    fromCreatedDate: $scope.data.filterOptions.fromCreatedDate,
                    toCreatedDate: $scope.data.filterOptions.toCreatedDate,
                    page: $scope.data.pagingOptions.currentPageDetails,
                    pageSize: $scope.data.pagingOptions.pageSize,
                    sortBy: $scope.data.sortOptions.fieldDetails,
                    sortDirection: ($scope.data.sortOptions.sortReverseDetails) ? 'desc' : 'asc'
                };

                servicesFactory.getInvoiceDetails(params)
                .then(function (response) {
                    $scope.invoiceDetails = response.data.contentDetails;
                    $scope.data.totalProductDetails = response.data.totalProductDetails;
                    $scope.data.totalNetAmountDetails = response.data.totalNetAmountDetails;
                    $scope.data.totalItemsDetails = response.data.totalRecordsDetails;
                    $scope.data.totalPagesDetails = response.data.totalPagesDetails;
                    $scope.data.pagingOptions.currentPageDetails = response.data.currentPageDetails;
                    // Fill Chart
                    /*
                    for (var i = 0; i < $scope.invoices.length; i++) {
                        $scope.lineChartLabels.push(moment($scope.invoices[i].createdDate).format('DD/MM/YYYY'));
                        $scope.lineChartData[0].push($scope.invoices[i].netTotal);
                    }
                    */
                    //
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
            $scope.updateViewByFilters = function () {
                $scope.getInvoicesByFilters();
                $scope.getInvoiceDetailsByFilters();
            }

            //Init all data
            $scope.getInvoicesByFilters();
            $scope.getInvoiceDetailsByFilters();
            //End init
        }
        ]);
})();
