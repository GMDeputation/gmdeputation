app.controller('appointmentsController', function ($scope, $window, $mdDialog, appointmentService) {
    $scope.selectApointment = [];
    $scope.accessCode = "";
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        name: '',
        fromDate: null,
        toDate: null,
        limit: 20,
        order: 'name',
        page: 1
    };
    $scope.selected = [];
    $scope.showFilter = false;
    $scope.data = [];

    $scope.filter = function () {
        $scope.showFilter = true;
    };
    $scope.close = function () {
        $scope.showFilter = false;

        $scope.query = {
            filter: '',
            fromDate: null,
            toDate: null,
            limit: 20,
            order: 'name',
            page: 1
        };
        $scope.search();
    };


    $scope.$watch('query.filter', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue !== oldValue) {
            $scope.query.page = 1;
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });
    $scope.$watch('query.fromDate', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue !== oldValue) {
            $scope.query.page = 1;
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });
    $scope.$watch('query.toDate', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue !== oldValue) {
            $scope.query.page = 1;
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });
    $scope.search = function () {
        $scope.promise = appointmentService.search($scope.query).then(success);
    };
    $scope.add = function () {
        $window.location.href = '/macroSchedule/approvedSchedule';
    };
    $scope.detail = function (id) {
        if (id === undefined) {
            id = '0';
        }
        $window.location.href = '/appointments/detail/' + id;
    };

    $scope.viewOnCalender = function () {
        $window.location.href = '/appointments/calender';
    };

    $scope.delete = function (id) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete Apointments?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.isDisabled = true;

            appointmentService.delete(id).then(function (resp) {
                if (resp !== null && resp !== undefined && resp === true) {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Apointments Delete successfully')
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $scope.isDisabled = false;
                        $scope.search();
                    });
                }
                else {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Failed To Delete Apointments')
                            .ok('OK')
                    ).then(function () {
                        $scope.isDisabled = false;
                    });
                }
            });

        }, function () {
            $mdDialog.hide();
        });
    };

    function success(resp) {
        $scope.appointments = resp.data.result;
        $scope.count = resp.data.count;

        $scope.accessCode = $scope.appointments.length > 0 ? $scope.appointments[0].accessCode : "";
    }

    $scope.approved = function () {
        if ($scope.selectApointment.length > 0) {
            var confirm = $mdDialog.confirm()
                .title('Church Admin')
                .textContent('Are You sure To Approved All Selected Apointments?')
                .ariaLabel('Alert Dialog')
                .cancel('No')
                .ok('Yes');

            $mdDialog.show(confirm).then(function () {
                $scope.isDisabled = true;
                if ($scope.accessCode === "P") {
                    appointmentService.approvedPastorApointmentsIds($scope.selectApointment).then(function (resp) {
                        if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                            $mdDialog.show(
                                $mdDialog.alert()
                                    .clickOutsideToClose(false)
                                    .title('Church Admin')
                                    .textContent('Apointments Approved successfully')
                                    .ariaLabel('Alert Dialog')
                                    .ok('OK')
                            ).then(function () {
                                $window.location.href = '/appointments';
                                $scope.isDisabled = false;
                            });
                        }
                        else {
                            $mdDialog.show(
                                $mdDialog.alert()
                                    .clickOutsideToClose(false)
                                    .title('Church Admin')
                                    .textContent('Failed To Approved Apointments')
                                    .ok('OK')
                            ).then(function () {
                                $scope.isDisabled = false;
                            });
                        }
                    });
                }
                else if ($scope.accessCode === "M") {
                    appointmentService.approvedMissionaryApointmentsIds($scope.selectApointment).then(function (resp) {
                        if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                            $mdDialog.show(
                                $mdDialog.alert()
                                    .clickOutsideToClose(false)
                                    .title('Church Admin')
                                    .textContent('Apointments Approved successfully')
                                    .ariaLabel('Alert Dialog')
                                    .ok('OK')
                            ).then(function () {
                                $window.location.href = '/appointments';
                                $scope.isDisabled = false;
                            });
                        }
                        else {
                            $mdDialog.show(
                                $mdDialog.alert()
                                    .clickOutsideToClose(false)
                                    .title('Church Admin')
                                    .textContent('Failed To Approved Apointments')
                                    .ok('OK')
                            ).then(function () {
                                $scope.isDisabled = false;
                            });
                        }
                    });
                }
            }, function () {
                $mdDialog.hide();
            });
        }
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Select Apointments')
                    .ok('OK')
            );
        }
    };

    function init() {
        $scope.search();
    }
    init();
});