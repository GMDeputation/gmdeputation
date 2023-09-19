app.controller('missionaryServiceReportController', function ($scope, $window, $mdDialog, userService, reportsService) {
    $scope.reportParams = {};

    $scope.searchUser = function (searchText) {
        $scope.user = [];

        if (searchText !== null && searchText !== undefined && searchText !== "") {
            searchText = searchText.toLowerCase();
        }

        angular.forEach($scope.users, function (event) {
            if (event.name.toLowerCase().match(searchText)) {
                $scope.user.push(event);
            }
        });

        return $scope.user;
    };
    $scope.selectedUserName = function ($item) {
        $scope.reportParams.userId = $item !== null && $item !== undefined ? $item.id : '0';
    };

    $scope.searchUserActivityData = function () {       

        reportsService.missionaryServiceRerport($scope.reportParams).then(function (resp) {
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
            reportsService.generateUserActivityReport($scope.searchDatas).then(processSuccess, processError);
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
            reportsService.generateUserActivityReportPdf($scope.searchDatas).then(processSuccess, processError);
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
            reportsService.generateUserActivityReportWord($scope.searchDatas).then(processSuccess, processError);
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
      
        $scope.searchDatas = [];
        userService.getAllMissionariesUser().then(function (resp) {
            $scope.users = resp;
        });

    }
    init();
});