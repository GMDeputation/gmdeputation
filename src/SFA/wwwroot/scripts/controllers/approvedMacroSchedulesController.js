app.controller('approvedMacroSchedulesController', function ($scope, $window, $mdDialog, macroScheduleService) {
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
        macroScheduleService.getAllapproved($scope.query).then(function (resp) {
            $scope.macroSchedules = resp.data;
        });
    };

    $scope.detail = function (macroScheduleDetailId) {      
        $window.location.href = '/appointments/add/' + macroScheduleDetailId;
    };

    $scope.back = function () {
        $window.location.href = '/appointments';
    };

    function init() {
        $scope.search();      
    }
    init();
});