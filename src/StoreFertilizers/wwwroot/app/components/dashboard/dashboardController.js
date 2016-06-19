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
            $scope.data = {
                totalNetAmount: 0,
                totalPages: 0,
                totalItems: 0,
                filterOptions: {
                    filterText: '',
                    isTax: false,
                    fromCreatedDate: new Date(totalDays.subtract(3, 'day')),
                    toCreatedDate: dateOffset,
                    dueIn: 'next3'
                },
                sortOptions: {
                    field: 'dueDate',
                    directions: ['desc', 'asc'],
                    sortReverse: true
                },
                pagingOptions: {
                    pageSizes: [20, 50, 100],
                    pageSize: 50,
                    currentPage: 1
                }
            };

            $scope.isShowSalesChart = true;
            $scope.isShowProductStockChart = true;

            //Line Chart
            $scope.lineChartLabels = ["ม.ค.", "ก.พ.", "ม.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค."];
            $scope.lineChartSeries = ['ปุ๋ย', 'ยา'];
            $scope.lineChartData = [
              [65, 59, 80, 81, 56, 55, 40],
              [28, 48, 40, 19, 86, 27, 90]
            ];
            // End Line Chart
            // Pie Chart
            $scope.pieChartLabels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
            $scope.pieChartData = [300, 500, 100];
            //End Pie

            //Bar Chart
            $scope.barChartLabels = ['กระต่าย', 'ยาร่า', 'OX-PREMIUM', 'ออติวา', 'ไธอะโนซาน', 'พรีมาตรอน', 'ไดเทสเอ็ม'];
            $scope.barChartSeries = ['สต๊อค', 'คงเหลือ'];

            $scope.barChartData = [
              [65, 59, 80, 81, 56, 55, 90],
              [28, 48, 40, 19, 26, 27, 40]
            ];
            //End Bar Chart

        }
        ]);
})();