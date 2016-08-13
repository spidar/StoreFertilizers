(function () {
    'use strict';

    angular
        .module('app')
        .controller('providersController', ['$scope', '$location', '$timeout', '$filter', 'servicesFactory',
        function ($scope, $location, $timeout, $filter, servicesFactory) {

            $scope.status = '';
            $scope.showLoading = false;
            $scope.data = {

                totalPages: 0,
                totalItems: 0,
                filterOptions: {
                    filterText: '',
                    externalFilter: 'searchText',
                    useExternalFilter: true
                },
                sortOptions: {
                    fields: [],
                    field: 'name',
                    directions: ['desc', 'asc'],
                    sortReverse: false
                },
                pagingOptions: {
                    pageSizes: [20, 50, 100],
                    pageSize: 50,
                    currentPage: 1
                }
            };

            $scope.providers = {};

            // Get All
            $scope.getProvidersByFilters = function () {
                if ($scope.isNewItem) {
                    $scope.reset($scope.selected);
                }
                $scope.showLoading = true;

                var params = {
                    searchtext: $scope.data.filterOptions.filterText,
                    page: $scope.data.pagingOptions.currentPage,
                    pageSize: $scope.data.pagingOptions.pageSize,
                    sortBy: $scope.data.sortOptions.field,
                    sortDirection: ($scope.data.sortOptions.sortReverse) ? 'desc' : 'asc'
                };

                servicesFactory.getProvidersByParams(params)
                .then(function (response) {
                    $scope.providers = response.data.content;
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
            };

            //Init all data
            (function init() {
                $scope.getProvidersByFilters();
            })()
            //End init
        }
        ]);
})();
