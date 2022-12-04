app.controller('churchWiseAppoinmentReportsController', function ($scope, $window, $mdDialog, reportsService, districtService, sectionService, churchService) {
    $scope.reportParams = {};

    $scope.searchDistrict = function (searchText) {
        $scope.district = [];

        if (searchText !== null && searchText !== undefined && searchText !== "") {
            searchText = searchText.toLowerCase();
        }

        angular.forEach($scope.districts, function (event) {
            if (event.name.toLowerCase().match(searchText)) {
                $scope.district.push(event);
            }
        });

        return $scope.district;
    };
    $scope.selectedDistrictName = function ($item) {
        $scope.reportParams.districtId = $item !== null && $item !== undefined ? $item.id : '0';
        $scope.churches = [];

        if ($scope.reportParams.districtId !== null && $scope.reportParams.districtId !== undefined && $scope.reportParams.districtId !== '0') {

            sectionService.getSectionByDistrictId($scope.reportParams.districtId).then(function (resp) {
                $scope.sections = resp;
            });

            $scope.reportParams.church = null;

            $scope.getAllChurchByDistrict($scope.reportParams.districtId);
        }
        else {
            $scope.getAllChurch();
        }
    };

    $scope.getAllChurchByDistrict = function (districtId) {
        churchService.getChurchByDistrict(districtId).then(function (resp) {
            $scope.churches = resp;
        });
        $scope.reportParams.church = null;
    };
    $scope.getAllChurchBySection = function () {
        if ($scope.reportParams.sectionId !== null && $scope.reportParams.sectionId !== undefined) {
            churchService.GetChurchBySectionId($scope.reportParams.sectionId).then(function (resp) {
                $scope.churches = resp;
            });

            $scope.reportParams.church = null;

        } else if ($scope.reportParams.districtId !== null && $scope.reportParams.districtId !== undefined && $scope.reportParams.districtId !== '0') {
            $scope.getAllChurchByDistrict($scope.reportParams.districtId);
        }
        else {
            $scope.getAllChurch();
        }
    };


    $scope.getAllChurch = function () {
        churchService.getAll().then(function (resp) {
            $scope.churches = resp.data;
        });

    };


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
        reportsService.getChurchWiseAppoinmentData($scope.reportParams).then(function (resp) {
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
            reportsService.generateChurchWiseAppoinmentReport($scope.searchDatas).then(processSuccess, processError);
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

        $scope.getAllChurch();

        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });
    }
    init();
});