app.controller('macroSchedulesController', function ($scope, $window, $mdDialog, macroScheduleService) {
    $scope.selectSchedule = [];
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        name: '',
        fromDate: null,
        toDate: null,
        fromEntryDate: null,
        toEntryDate: null,
        limit: 20,
        order: 'filter',
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
            fromEntryDate: null,
            toEntryDate: null,
            limit: 20,
            order: 'filter',
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

    $scope.$watch('query.fromEntryDate', function (newValue, oldValue) {
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
    $scope.$watch('query.toEntryDate', function (newValue, oldValue) {
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
        $scope.promise = macroScheduleService.search($scope.query).then(success);
    };

    $scope.detail = function (id) {
        if (id === undefined) {
            id = '0';
        }
        $window.location.href = '/macroSchedule/detail/' + id;
    };

    $scope.edit = function (id) {
        $window.location.href = '/macroSchedule/edit/' + id;
    };

    $scope.addFromCalender = function () {
        $window.location.href = '/macroSchedule/calender';
    };
    $scope.import = function () {
        $window.location.href = '/macroSchedule/import';
    };

    function success(resp) {
        $scope.selectSchedule = [];

        $scope.macroSchedules = resp.data.result;
        $scope.count = resp.data.count;
    }

    $scope.approved = function () {
        if ($scope.selectSchedule.length > 0) {
            var confirm = $mdDialog.confirm()
                .title('Church Admin')
                .textContent('Are You sure To Approved All Selected Schedules?')
                .ariaLabel('Alert Dialog')
                .cancel('No')
                .ok('Yes');

            $mdDialog.show(confirm).then(function () {
                $scope.isDisabled = true;

                macroScheduleService.approvedMacroSchedulesIds($scope.selectSchedule).then(function (resp) {
                    if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                        $mdDialog.show(
                            $mdDialog.alert()
                                .clickOutsideToClose(false)
                                .title('Church Admin')
                                .textContent('Macro Schedules Approved successfully')
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
                                .textContent('Failed To Approved Macro Schedules')
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
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Select Macro Schedules')
                    .ok('OK')
            );
        }
    };

    $scope.rejected = function () {
        if ($scope.selectSchedule.length > 0) {
            var confirm = $mdDialog.confirm()
                .title('Church Admin')
                .textContent('Are You sure To Reject All Selected Schedules?')
                .ariaLabel('Alert Dialog')
                .cancel('No')
                .ok('Yes');

            $mdDialog.show(confirm).then(function () {
                $scope.isDisabled = true;

                macroScheduleService.rejectMacroSchedulesIds($scope.selectSchedule).then(function (resp) {
                    if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                        $mdDialog.show(
                            $mdDialog.alert()
                                .clickOutsideToClose(false)
                                .title('Church Admin')
                                .textContent('Macro Schedules Rejected successfully')
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
                                .textContent('Failed To Rejected Macro Schedules')
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
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Select Macro Schedules')
                    .ok('OK')
            );
        }
    };

    function init() {
        $scope.search();
    }
    init();
});