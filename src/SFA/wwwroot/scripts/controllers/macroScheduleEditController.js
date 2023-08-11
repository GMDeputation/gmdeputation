app.controller('macroScheduleEditController', function ($scope, $window, $location, $mdDialog, macroScheduleService, districtService, userService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('edit/') + 5);
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

    $scope.openDialog = function () {
        $mdDialog.show(
            $mdDialog.prompt()
                .clickOutsideToClose(false)
                .title('Cancellation Confirmation')
                .textContent('Are you sure you want to cancel?')
                .ariaLabel('Alert Dialog')
                .placeholder("Reason for cancelling")
                .ok('OK')
                .cancel("Cancel")
        ).then(function (result) {
            $scope.macroScheduleDetails.Cancellation_Notes = result
            macroScheduleService.cancel($scope.macroScheduleDetails);
            $window.location.href = '/macroSchedule';
        });

    };

    $scope.selectedDistrictName = function ($item) {
        $scope.macroScheduleDetails.districtId = $item !== null && $item !== undefined ? $item.id : '0';

        //$scope.users = [];
        //if ($scope.macroScheduleDetails.districtId !== null && $scope.macroScheduleDetails.districtId !== undefined && $scope.macroScheduleDetails.districtId !== '0') {
        //    userService.getAllMissionariesByDistrict($scope.macroScheduleDetails.districtId).then(function (resp) {
        //        $scope.users = resp;
        //    });
        //}
    };


    $scope.save = function () {
        $scope.isDisabled = true;

        if ($scope.macroScheduleDetails.districtId === null || $scope.macroScheduleDetails.districtId === undefined || $scope.macroScheduleDetails.districtId === '0') {
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
     
        macroScheduleService.edit($scope.macroScheduleDetails).then(function (resp) {
            if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Macro Schedule Updated successfully')
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $window.location.reload();
                    $scope.isDisabled = false;
                });
            }
            else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Failed To Updated Macro Schedule')
                        .ok('OK')
                ).then(function () {
                    $scope.isDisabled = false;
                });
            }
        });
    };

    $scope.approved = function () {
        if ($scope.macroScheduleDetails.approvedRejectRemarks === null || $scope.macroScheduleDetails.approvedRejectRemarks === undefined) {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please give a Remarks')
                    .ok('OK')
            );
        }
        else {
            var confirm = $mdDialog.confirm()
                .title('Church Admin')
                .textContent('Are You sure To Approved?')
                .ariaLabel('Alert Dialog')
                .cancel('No')
                .ok('Yes');

            $mdDialog.show(confirm).then(function () {
                $scope.isDisabled = true;

                macroScheduleService.approved($scope.macroScheduleDetails).then(function (resp) {
                    if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                        $mdDialog.show(
                            $mdDialog.alert()
                                .clickOutsideToClose(false)
                                .title('Church Admin')
                                .textContent('Macro Schedule Approved successfully')
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
                                .textContent('Failed To Approved Macro Schedule')
                                .ok('OK')
                        ).then(function () {
                            $scope.isDisabled = false;
                        });
                    }
                });
            }, function () {
                $mdDialog.hide();
            });
        }
    };

    $scope.rejected = function () {
        if ($scope.macroScheduleDetails.approvedRejectRemarks === null || $scope.macroScheduleDetails.approvedRejectRemarks === undefined) {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please give a Remarks')
                    .ok('OK')
            );
        }
        else {
            var confirm = $mdDialog.confirm()
                .title('Church Admin')
                .textContent('Are You sure To Reject?')
                .ariaLabel('Alert Dialog')
                .cancel('No')
                .ok('Yes');

            $mdDialog.show(confirm).then(function () {
                $scope.isDisabled = true;

                macroScheduleService.rejected($scope.macroScheduleDetails).then(function (resp) {
                    if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                        $mdDialog.show(
                            $mdDialog.alert()
                                .clickOutsideToClose(false)
                                .title('Church Admin')
                                .textContent('Macro Schedule Rejected successfully')
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
                                .textContent('Failed To Rejected Macro Schedule')
                                .ok('OK')
                        ).then(function () {
                            $scope.isDisabled = false;
                        });
                    }
                });
            }, function () {
                $mdDialog.hide();
            });
        }
    };

    function init() {
        $scope.minDate = new Date();

        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });
        userService.getAllMissionariesUser().then(function (resp) {
            $scope.users = resp;
        });

        macroScheduleService.getMacroScheduleDetailsById(id).then(function (data) {
            $scope.macroScheduleDetails = data;
            $scope.macroScheduleDetails.startDateOld = $scope.macroScheduleDetails.startDate
            $scope.macroScheduleDetails.endDateOld = $scope.macroScheduleDetails.endDate
            $scope.macroScheduleDetails.userIdOld = $scope.macroScheduleDetails.userId

            $scope.district = { "id": $scope.macroScheduleDetails.districtId, "name": $scope.macroScheduleDetails.districtName };

            //userService.getAllMissionariesByDistrict($scope.macroScheduleDetails.districtId).then(function (resp) {
            //    $scope.users = resp;
            //});
        });


    }
    init();
});