(function () {
    'use strict';

    angular.module('app', [
        // Angular modules 
        //'ngRoute',

        // Custom modules 
        //'servicesFactory'
        // 3rd Party Modules
        'ngMaterial'
    ]).config(function ($mdDateLocaleProvider) {

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
            var offset = moment(date).utcOffset();
            var dateOffset = new Date(moment(date).add(offset, 'm'));
            //return moment(dateOffset).format('DD/MM/YYYY');
            var m = moment(dateOffset).format('DD/MM/YYYY');
            return m;
        };

    }).config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('default')
          .primaryPalette('green')
          .accentPalette('orange');
    });

})();