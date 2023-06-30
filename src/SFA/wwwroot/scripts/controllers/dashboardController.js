app.controller('DashBoardController', function ($scope, $filter, $window, $location, $mdDialog, $interval, dashboardService) {
    function init() {
        dashboardService.getCount().then(function (resp) {
            $scope.macSched = resp.data;
        });
    }
    init();
});