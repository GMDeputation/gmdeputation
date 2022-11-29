//angular.module('app.config', [])
//    .constant('SETTINGS', {
//        PAGE_SIZE: 20,
//        SORTING_DOWN_ICON: "fa-chevron-circle-down",
//        SORTING_UP_ICON: "fa-chevron-circle-up",
//        ACTIVE_ICON: "fa-check-square-o",
//        INACTIVE_ICON: "fa-square-o",
//        ADD_TITLE: "ADD",
//        UPDATE_TITLE: "EDIT",
//        ADD_BUTTON_TEXT: "Save",
//        UPDATE_BUTTON_TEXT: "Update",
//        API_URL: 'http://192.168.1.70/admin/"
//    });
var app = angular.module('app', ['ngAnimate', 'ngAria', 'ngMessages', 'ngMaterial', 'pascalprecht.translate', 'angularTreeview', 'md.data.table', 'lfNgMdFileInput', 'app.directives','md.time.picker']);
app.config(function ($mdDateLocaleProvider) {
    $mdDateLocaleProvider.formatDate = function (date) {
        if (!date) { return ''; }
        else {
            return moment(date).format('DD-MM-YYYY');
        }

    };
    $mdDateLocaleProvider.parseDate = function (dateString) {
        var m = moment(dateString, 'DD-MM-YYYY', true);
        return m.isValid() ? m.toDate() : new Date(NaN);
    };
});
app.config(function ($translateProvider) {
    $translateProvider
        .useStaticFilesLoader({
            prefix: '/locales/locale-',
            suffix: '.json'
        })
        // remove the warning from console log by putting the sanitize strategy
        .useSanitizeValueStrategy('sanitizeParameters')
        .preferredLanguage('en');
});
var capitalizeDirective = function () {
    return {
        restrict: 'A',
        require: "?ngModel",
        link: function (scope, element, attributes, ngModel) {
            var caretPos,
                capitalize = function (inputValue) {
                    caretPos = element[0].selectionStart;
                    return (inputValue || '').toUpperCase();
                };
            if (ngModel) {
                ngModel.$formatters.push(capitalize);
                ngModel._$setViewValue = ngModel.$setViewValue;
                ngModel.$setViewValue = function (val) {
                    ngModel._$setViewValue(capitalize(val));
                    ngModel.$render();
                    element[0].selectionStart = caretPos;
                    element[0].selectionEnd = caretPos;
                };
            } else {
                element.val(capitalize(element.val()));
                element.on("keypress keyup", function () {
                    scope.$evalAsync(function () {
                        element.val(capitalize(element.val()));
                    });
                });
            }
        }
    };
};
app.directive('capitalize', capitalizeDirective);
//app.filter('notInArray', function ($filter) {
//    return function (list, arrayFilter) {
//        if (arrayFilter) {
//            return $filter("filter")(list, function (listItem) {
//                return arrayFilter.indexOf(listItem) === -1;
//            });
//        }
//    };
//});