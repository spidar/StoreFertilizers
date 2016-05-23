(function () {
    'use strict';

    angular.module('app', [
        // Angular modules 
        'ngRoute',

        // Custom modules 
        //'servicesFactory'
        // 3rd Party Modules
        'ngMaterial',
        'chart.js',
        'ui.bootstrap.pagination'
    ])
/*
    .config(['$routeProvider', '$locationProvider', '$httpProvider', function ($routeProvider, $locationProvider, $httpProvider) {

        // TODO use html5 *no hash) where possible
        // $locationProvider.html5Mode(true);

        $routeProvider.when('/', {
            templateUrl:'app/components/dashboard/dashboard.html'
        });

        $routeProvider.when('/invoicesjs', {
            templateUrl: 'app/components/invoices/invoices.html',
            controller: 'invoicesController'
        });

        $routeProvider.when('/contact', {
            templateUrl:'partials/contact.html'
        });
        $routeProvider.when('/about', {
            templateUrl:'partials/about.html'
        });
        $routeProvider.when('/faq', {
            templateUrl:'partials/faq.html'
        });

        // note that to minimize playground impact on app.js, we
        // are including just this simple route with a parameterized 
        // partial value (see playground.js and playground.html)
        $routeProvider.when('/playground/:widgetName', {
            templateUrl:'playground/playground.html',
            controller:'PlaygroundCtrl'
        });

        // by default, redirect to site root
        $routeProvider.otherwise({
            redirectTo:'/error.html'
        });

    }])
*/
     /*   
    .config(function ($mdDateLocaleProvider) {

        // Example of a French localization.
        $mdDateLocaleProvider.months = ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'];
        $mdDateLocaleProvider.shortMonths = ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'];
        $mdDateLocaleProvider.days = ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัส', 'ศุกร์', 'เสาร์', 'อาทิตย์'];
        $mdDateLocaleProvider.shortDays = ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส', 'อา'];

        // Example uses moment.js to parse and format dates.
        $mdDateLocaleProvider.parseDate = function (dateString) {
            var m = moment(dateString, 'DD/MM/YYYY', true);
            return m.isValid() ? m.toDate() : new Date(NaN);
        };
        $mdDateLocaleProvider.formatDate = function (date) {
            if(!date){
                return date;
            }
            return moment(date).format('DD/MM/YYYY');
        };

    })
    */
    .config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('default')
          .primaryPalette('green')
          .accentPalette('orange');
    }).config(function (ChartJsProvider) {
        //ChartJsProvider.setOptions({ colours: ['#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'] });
        ChartJsProvider.setOptions({ colours: ['#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'] });
    }).config(function (paginationConfig) {
        paginationConfig.boundaryLinks = true;
        paginationConfig.rotate = false;
        paginationConfig.maxSize = 5;
        paginationConfig.itemsPerPage = 20;
        paginationConfig.firstText = '«';
        paginationConfig.previousText = '‹';
        paginationConfig.nextText = '›';
        paginationConfig.lastText = '»';
    });

})();