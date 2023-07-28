app.controller('DashBoardController', function ($scope, $filter, $window, $location, $mdDialog, $interval, dashboardService) {

    function init() {
        dashboardService.getMissionarySummary().then(function (resp) {
            $scope.summary = resp.data;
        });
        dashboardService.getCount().then(function (resp) {
            $scope.macros = resp.data;
        });
        dashboardService.getServiceOneYearCount().then(function (resp) {
            $scope.services = resp.data;
        });
        dashboardService.getMacroScheduleThirtyDayCount().then(function (resp) {
            $scope.upcoming = resp.data;
        });
    }
    init();

});

