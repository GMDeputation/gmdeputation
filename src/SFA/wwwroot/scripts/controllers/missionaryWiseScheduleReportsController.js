app.controller('missionaryWiseScheduleReportsController', function ($scope, $window, $mdDialog, reportsService) {
    $scope.reportParams = {};

    $scope.search = function () {
        //$scope.reportParams.eventFromDate = new Date($scope.reportParams.eventFromDate).toUTCString();
        //$scope.reportParams.eventToDate = new Date($scope.reportParams.eventToDate).toUTCString();

        reportsService.getMissionaryScheduleData($scope.reportParams).then(function (resp) {
            $scope.searchDatas = resp.data;
        });
    };

    $scope.generateExcell = function () {
        if ($scope.searchDatas.length === 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Data Not Found')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            );
        }
        else {
            reportsService.generateMissionaryScheduleReport($scope.searchDatas).then(processSuccess, processError);
        }
    };

    $scope.generatePdf = function () {
        if ($scope.searchDatas.length === 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Data Not Found')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            );
        }
        else {
            reportsService.generateMissionaryScheduleReportPdf($scope.searchDatas).then(processSuccess, processError);
        }
    };

    $scope.generateWord = function () {
        if ($scope.searchDatas.length === 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Data Not Found')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            );
        }
        else {
            reportsService.generateMissionaryScheduleReportWord($scope.searchDatas).then(processSuccess, processError);
        }
    };
    function processSuccess(obj) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Data Download Successfully.')
                .ariaLabel('Alert Dialog')
                .ok('OK')
        );
    }

    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Download')
                .ok('OK')
        );
    }
    function init() {
        $scope.weeks = ["Previous Week","Current Week", "Next Week"];

        $scope.searchDatas = [];        
    }
    init();
});