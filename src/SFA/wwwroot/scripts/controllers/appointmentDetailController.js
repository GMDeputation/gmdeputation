app.controller('appointmentDetailController', function ($scope, $filter, $window, $location, $mdDialog, appointmentService, churchService, churchServiceTimeService, macroScheduleService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);
    $scope.isDiabled = false;
    $scope.backToList = function () {
        $window.location.href = '/appointments';
    };

    $scope.appointment = {
        serviceTime: new Date()
    };

    $scope.message = {
        hour: 'Hour is required',
        minute: 'Minute is required',
        meridiem: 'Meridiem is required'
    };

    $scope.save = function () {
        $scope.isDiabled = true;

        $scope.time = $filter('date')($scope.appointment.eventTime, 'HH:mm:ss');
        $scope.appointment.eventTime = $scope.time;

        appointmentService.save($scope.appointment).then(processSuccess, processError);
    };

    $scope.searchChurch = function (searchText) {
        if ($scope.appointment.eventDate !== null && $scope.appointment.eventDate !== undefined) {
            $scope.test = [];

            angular.forEach($scope.churches, function (event) {

                if (event.churchName.toLowerCase().match(searchText.toLowerCase())) {
                    $scope.test.push(event);
                }

            });

            return $scope.test;
        } else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Select Appointment Date')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            );

            return false;
        }
    };
    $scope.selectedChurchName = function ($item) {
        $scope.appointment.churchId = $item !== null && $item !== undefined ? $item.id : '0';

        if ($scope.appointment.churchId !== null && $scope.appointment.churchId !== undefined && $scope.appointment.churchId !== '0') {

            $scope.appointment.eventDate = new Date($scope.appointment.eventDate);
            var day = $scope.appointment.eventDate.getDay();

            angular.forEach($scope.days, function (detail) {
                if (detail.id === day) {
                    $scope.weekday = detail.name;
                }
            });

            churchServiceTimeService.getTimeByChurch($scope.appointment.churchId, $scope.weekday).then(function (resp) {
                $scope.times = resp;
            });
        }
    };

    $scope.dateChange = function () {
        $scope.church = null;
        $scope.times = [];
        $scope.appointment.eventTime = null;
    };

    $scope.submit = function () {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Submit?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
        $scope.isDiabled = true;

        appointmentService.submitAppointment($scope.appointment).then(function (resp) {
            if (resp.data !== null && resp.data !== undefined && resp.data === '') {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent("Appointment Submit successfully")
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $scope.isDiabled = false;
                    $window.location.href = '/appointments';
                });
            }
            else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent("!Failed to Appointment Submit")
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $scope.isDiabled = false;
                });
            }
            });
        }, function () {
            $mdDialog.hide();
        });
    };

    $scope.acceptPastor = function () {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Accept?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
        $scope.isDiabled = true;

            appointmentService.acceptAppointmentByPastor($scope.appointment).then(function (resp) {
            if (resp.data !== null && resp.data !== undefined && resp.data === '') {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent("Appointment Accepted successfully")
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $scope.isDiabled = false;
                    $window.location.href = '/appointments';
                });
            }
            else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent("!Failed to Appointment Accepted")
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $scope.isDiabled = false;
                });
            }
            });
        }, function () {
            $mdDialog.hide();
        });
    };

    $scope.forwardMissionary = function () {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Send?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
        $scope.isDiabled = true;

            appointmentService.forwardAppointmentForMissionary($scope.appointment).then(function (resp) {
            if (resp.data !== null && resp.data !== undefined && resp.data === '') {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent("Appointment Sending successfully")
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $scope.isDiabled = false;
                    $window.location.href = '/appointments';
                });
            }
            else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent("!Failed to Appointment Sending")
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $scope.isDiabled = false;
                });
            }
            });
        }, function () {
            $mdDialog.hide();
        });
    };

    $scope.acceptMissionary = function () {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Accept?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.isDiabled = true;

            appointmentService.acceptAppointmentByMissionary($scope.appointment).then(function (resp) {
                if (resp.data !== null && resp.data !== undefined && resp.data === '') {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent("Appointment Accepted successfully")
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $scope.isDiabled = false;
                        $window.location.href = '/appointments';
                    });
                }
                else {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent("!Failed to Appointment Accepted")
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $scope.isDiabled = false;
                    });
                }
            });
        }, function () {
            $mdDialog.hide();
        });
    };

    function init() {
        $scope.appointment = [];
        churchService.getAll().then(function (resp) {
            $scope.churches = resp.data;
        });

        $scope.days = [{ "id": 0, "name": "Sunday" }, { "id": 1, "name": "Monday" }, { "id": 2, "name": "Tuesday" }, { "id": 3, "name": "Wednesday" },
                        { "id": 4, "name": "Thurseday" }, { "id": 5, "name": "Friday" }, { "id": 6, "name": "Saturday" }];

        $scope.appointment = {};
        if (id !== null && id !== undefined && id !== '0') {
            appointmentService.get(id).then(function (resp) {
                $scope.appointment = resp;               

                if ($scope.appointment.churchId !== null && $scope.appointment.churchId !== undefined && $scope.appointment.churchId !== '0') {

                    $scope.church = { "id": $scope.appointment.churchId, "churchName": $scope.appointment.churchName };
                }

                macroScheduleService.getMacroScheduleDetailsById($scope.appointment.macroScheduleDetailId).then(function (resp) {
                    $scope.macroScheduleDetails = resp;

                    $scope.minDate = new Date($scope.macroScheduleDetails.startDate);
                    $scope.maxDate = new Date($scope.macroScheduleDetails.endDate);
                });
            });
        }
    }
    init();
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Appointment Updated successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $scope.isDiabled = false;
                //$window.location.href = '/appointments';
                $window.location.reload();
            });
        }
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent(obj.data)
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $scope.isDiabled = false;
            });
        }

    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Update Appointment')
                .ok('OK')
        ).then(function () {
            $scope.isDiabled = false;
        });
    }
});