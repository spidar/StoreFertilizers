(function() {
    'use strict';

    angular.module('app').directive('datelocaleprovider', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                $(function () {
                    
                    var toView = function (val) {
                        var offset = moment(val).utcOffset();
                        var date = new Date(moment(val).add(offset, 'm'));
                        //var formatdate = moment(date).format('DD/MM/YYYY');
                        //return date ? moment(date).format('DD/MM/YYYY') : '';
                        return val;
                    };

                    var toModel = function (val) {
                        var offset = moment(val).utcOffset();
                        var date = new Date(moment(val).add(offset, 'm'));
                        //var formatdate = moment(date).format('DD/MM/YYYY');
                        return date;
                    };

                    ngModelCtrl.$formatters.push(toView);
                    ngModelCtrl.$parsers.push(toModel);

                    /*
                    element.datepicker({
                        //language: 'th-th',
                        dateFormat: 'dd/mm/yyyy',
                        //setDate: null,
                        autoclose: true,
                        onSelect: function (date) {
                            ngModelCtrl.$setViewValue(date);
                            scope.$apply();
                        }
                    });
                    scope.$watch(attrs.ngModel, function (val) {
                        if (!val) {
                            return false;
                        }
                        //console.log(val);
                        var date = new Date(val);
                        element.datepicker("setDate", date);
                    });
                    */
                });
            }
        }
    }]);

})();