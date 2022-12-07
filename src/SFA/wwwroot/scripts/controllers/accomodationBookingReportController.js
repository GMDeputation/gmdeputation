app.controller('accomodationBookingReportController', function ($scope, $window, $mdDialog, reportsService, churchService) {
    $scope.reportParams = {};

    $scope.searchChurch = function (searchText) {
        $scope.test = [];

        if (searchText !== null && searchText !== undefined && searchText !== "") {
            searchText = searchText.toLowerCase();
        }

        angular.forEach($scope.churches, function (event) {
            if (event.churchName.toLowerCase().match(searchText)) {
                $scope.test.push(event);
            }
        });

        return $scope.test;
    };
    $scope.selectedChurchName = function ($item) {
        $scope.reportParams.churchId = $item !== null && $item !== undefined ? $item.id : '0';

    };

    $scope.search = function () {
        reportsService.getAccomodationBookingReportData($scope.reportParams).then(function (resp) {
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
            reportsService.generateAccomodationBookingReport($scope.searchDatas).then(processSuccess, processError);
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
            reportsService.generateAccomodationBookingReportPdf($scope.searchDatas).then(processSuccess, processError);
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
            reportsService.generateAccomodationBookingReportWord($scope.searchDatas).then(processSuccess, processError);
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

        churchService.getAllByUser().then(function (resp) {
            $scope.churches = resp.data;
        });
    }
    init();
});