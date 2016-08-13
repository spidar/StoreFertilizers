(function () {
    'use strict';

    angular
        .module('app')
        .controller('dashboardController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            var today = new Date();
            var val = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
            var offset = moment(val, 'DD/MM/YYYY').utcOffset();
            var dateOffset = new Date(moment(val, 'DD/MM/YYYY').add(offset, 'm'));
            var totalDays = moment(val, 'DD/MM/YYYY').add(offset, 'm');
            $scope.showLoading = false;
            $scope.status = '';
            $scope.data = {
                totalNetAmount: 0,
                totalNetPaidAmount: 0,
                totalNetUnPaidAmount: 0,
                totalNetUnPaidAmountInSystem: 0,
                totalPages: 0,
                totalItems: 0,
                purchaseVsSaleChartLabels: [],
                purchaseVsSaleChartSeries: [],
                purchaseVsSaleChartData: [],
                stockPieChartLabels: [],
                stockPieChartData: [],
                filterOptions: {
                    filterText: '',
                    isTax: 'notax',
                    fromCreatedDate: null,
                    toCreatedDate: null,
                    dueIn: 'todaytomorrow'
                },
                sortOptions: {
                    field: 'dueDate',
                    directions: ['desc', 'asc'],
                    sortReverse: false
                },
                pagingOptions: {
                    pageSizes: [20, 50, 100],
                    pageSize: 50,
                    currentPage: 1
                }
            };

            $scope.invoices = {};
            $scope.notifications = {};
            $scope.stockNotifications = {};

            $scope.isShowSalesChart = true;
            $scope.isShowProductStockChart = true;

            //Line Chart
            /*
            $scope.data.purchaseVsSaleChartLabels = ["ม.ค.", "ก.พ.", "ม.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค."];
            $scope.data.purchaseVsSaleChartSeries = ['ปุ๋ย', 'ยา'];
            $scope.data.purchaseVsSaleChartData = [
              [65, 59, 80, 81, 56, 55, 40],
              [28, 48, 40, 19, 86, 27, 90]
            ];
            */
            // End Line Chart
            // Pie Chart
            //$scope.pieChartLabels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
            //$scope.pieChartData = [300, 500, 100];
            //End Pie

            //Bar Chart
            /*
            $scope.barChartLabels = ['กระต่าย', 'ยาร่า', 'OX-PREMIUM', 'ออติวา', 'ไธอะโนซาน', 'พรีมาตรอน', 'ไดเทสเอ็ม'];
            $scope.barChartSeries = ['สต๊อค', 'คงเหลือ'];

            $scope.barChartData = [
              [65, 59, 80, 81, 56, 55, 90],
              [28, 48, 40, 19, 26, 27, 40]
            ];
            */
            //End Bar Chart

            $scope.getDashboardData = function () {
                $scope.showLoading = true;
                servicesFactory.getDashboardData()
                .then(function (response) {
                    $scope.notifications = response.data.notifications;
                    $scope.stockNotifications = response.data.stockNotifications;
                    $scope.data.totalNetAmount = response.data.totalNetAmount;
                    $scope.data.totalNetPaidAmount = response.data.totalNetPaidAmount;
                    $scope.data.totalNetUnPaidAmount = response.data.totalNetUnPaidAmount;
                    $scope.data.totalNetUnPaidAmountInSystem = response.data.totalNetUnPaidAmountInSystem;

                    $scope.data.purchaseVsSaleChartLabels = response.data.purchaseVsSaleChartLabels;
                    $scope.data.purchaseVsSaleChartSeries = response.data.purchaseVsSaleChartSeries;
                    $scope.data.purchaseVsSaleChartData = response.data.purchaseVsSaleChartData;

                    $scope.data.stockPieChartLabels = response.data.stockPieChartLabels;
                    $scope.data.stockPieChartData = response.data.stockPieChartData;

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
            };

            //Init all data
            (function init() {
                $scope.getDashboardData();
            })()
            //End init


        }
        ]);
})();