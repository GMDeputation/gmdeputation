app.controller('macroScheduleDetailController', function ($scope, $window, $location, $mdDialog, macroScheduleService, districtService, userService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);
    $scope.districtIdNull = false;
    $scope.backToMain = function () {
        $window.location.href = '/home';
    };
    $scope.backToList = function () {
        $window.location.href = '/macroSchedule';
    };

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
    $scope.selectedDistrictName = function ($item, index) {
        $scope.macroSchedule.macroScheduleDetails[index].districtId = $item !== null && $item !== undefined ? $item.id : '0';
        //$scope.macroSchedule.macroScheduleDetails[index].users = [];

        //if ($scope.macroSchedule.macroScheduleDetails[index].districtId !== null && $scope.macroSchedule.macroScheduleDetails[index].districtId !== undefined && $scope.macroSchedule.macroScheduleDetails[index].districtId !== '0') {
        //    userService.getAllMissionariesUser().then(function (resp) {
        //        $scope.macroSchedule.macroScheduleDetails[index].users = resp;
        //    });
        //}
    };

    $scope.addNewRow = function () {
        $scope.macroSchedule.macroScheduleDetails = $scope.macroSchedule.macroScheduleDetails.concat({ "district": null, "userId": null, "users": [], "startDate": new Date(), "endDate": new Date() });
    };
    $scope.deleteRow = function (index) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.macroSchedule.macroScheduleDetails.splice(index, 1);
        }, function () {
            $mdDialog.hide();
        });
    };


    $scope.save = function () {
        $scope.isDisabled = true;

        angular.forEach($scope.macroSchedule.macroScheduleDetails, function (detail) {
            if (detail.districtId === null || detail.districtId === undefined || detail.districtId === '0') {
                $scope.districtIdNull = true;
            }
            else { $scope.districtIdNull = false; }
        });
        if ($scope.districtIdNull) {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Search And Select correct District')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $scope.isDisabled = false;
            });
            return false;
        }

        macroScheduleService.save($scope.macroSchedule).then(function (resp) {
            if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Macro Schedule Saved successfully')
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $window.location.href = '/macroSchedule';
                    $scope.isDisabled = false;
                });
            }
            else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Failed To Saved Macro Schedule')
                        .ok('OK')
                ).then(function () {
                    $scope.isDisabled = false;
                });
            }
        });
    };

    function init() {
        $scope.minDate = new Date();

        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });
        userService.getAllMissionariesUser().then(function (resp) {
            $scope.users = resp;
        });

        if (id !== null && id !== undefined && id !== '0') {
            macroScheduleService.get(id).then(function (data) {
                $scope.macroSchedule = data;

                if ($scope.macroSchedule.macroScheduleDetails.length > 0) {
                    angular.forEach($scope.macroSchedule.macroScheduleDetails, function (detail) {
                        detail.district = { "id": detail.districtId, "name": detail.districtName };

                        //userService.getAllMissionariesByDistrict(detail.districtId).then(function (resp) {
                        //    detail.users = resp;
                        //});
                    });
                }
            });
        }
        else {
            $scope.macroSchedule = { "entryDate": new Date(), "macroScheduleDetails": [{ "district": null, "userId": null, "users": [], "startDate": new Date(), "endDate": new Date() }] };
        }
    }
    init();
});