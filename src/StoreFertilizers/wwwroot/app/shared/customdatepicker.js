(function() {
    'use strict';

    angular.module('app').directive('customdatepicker', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                $(function () {
                    var options = {
                        format: "DD/MM/YYYY",
                    };
                    element.datetimepicker(options);
                    element.bind('blur keyup change', function () {
                        scope.$apply(function () {                            
                            var value = element.val();
                            if (value.trim() == '') {
                                ngModelCtrl.$setViewValue('');
                                return;
                            }
                            var offset = moment(value, 'DD/MM/YYYY').utcOffset();
                            var dateOffset = new Date(moment(value, 'DD/MM/YYYY').add(offset, 'm'));
                            if (!!dateOffset) {
                                ngModelCtrl.$setViewValue(dateOffset);
                            }else
                            {
                                element.val('Invalid Date');
                                ngModelCtrl.$setViewValue('');
                            }
                        });
                    });
                    var toView = function (val) {
                        if (!val) {
                            return '';
                        }
                        return moment(val, 'YYYY-MM-DD').format('DD/MM/YYYY');
                    };
                    ngModelCtrl.$formatters.push(toView);
                    scope.$watch(attrs.ngModel, function (val) {
                        if (!val) {
                            return '';
                        }
                        //console.log(val);
                        element.val(moment(val).format('DD/MM/YYYY'));
                    });
                    /*
                    var toView = function (val) {
                        var offset = moment(val).utcOffset();
                        var date = new Date(moment(val).add(offset, 'm'));
                        //var formatdate = moment(date).format('DD/MM/YYYY');
                        return date ? moment(date).format('DD/MM/YYYY') : '';
                    };

                    var toModel = function (val) {
                        var offset = moment(val).utcOffset();
                        var date = new Date(moment(val).add(offset, 'm'));
                        //var formatdate = moment(date).format('DD/MM/YYYY');
                        return date;
                    };

                    ngModelCtrl.$formatters.push(toView);
                    ngModelCtrl.$parsers.push(toModel);
                    */
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