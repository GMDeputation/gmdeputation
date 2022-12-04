app.controller('userActivityReportsController', function ($scope, $window, $mdDialog, reportsService, userService, roleService) {
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
        $scope.reportParams.fromDate = new Date($scope.reportParams.fromDate).toUTCString();
        $scope.reportParams.toDate = new Date($scope.reportParams.toDate).toUTCString();

        reportsService.getUserActivityData($scope.reportParams).then(function (resp) {
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
        $scope.actions = ["LOG IN", "LOG OUT", "APPOINTMENT CREATE", "APPOINTMENT SUBMIT", "APPOINTMENT UPDATE", "APPOINTMENT ACCEPT BY PASTOR", "APPOINTMENT ACCEPT BY MISSIONARY",
                        "CHURCH CREATE", "CHURCH UPDATE", "CHURCH SERVICE TIME CREATE", "CHURCH SERVICE TIME UPDATE", "DISTRICT CREATE", "DISTRICT UPDATE", "SECTION CREATE",
                        "SECTION UPDATE", "STATE CREATE", "STATE UPDATE", "MACRO SCHEDULE CREATE", "MACRO SCHEDULE UPDATE", "MACRO SCHEDULE APPROVE", "MACRO SCHEDULE REJECT"];

        $scope.searchDatas = [];
        userService.getAll().then(function (resp) {
            $scope.users = resp.data;
        });
        roleService.getAll().then(function (resp) {
            $scope.roles = resp.data;
        });
    }
    init();
});