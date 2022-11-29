app.controller('churchServiceTimesController', function ($scope, $filter, $window, $mdDialog, serviceTypeService, churchServiceTimeService, churchService) {
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        filter: '',
        churchId: null,
        serviceTypeId: [],
        weekDay: [],
        startTime: null,
        endTime: null,
        limit: 20,
        order: 'name',
        page: 1
    };
    $scope.selected = [];
    $scope.showFilter = false;
    $scope.data = [];
    $scope.selectedWeekDay = [];
    $scope.message = {
        hour: 'Hour is required',
        minute: 'Minute is required',
        meridiem: 'Meridiem is required'
    };

    $scope.filter = function () {
        $scope.showFilter = true;
    };
    $scope.close = function () {
        $scope.showFilter = false;

        $scope.query = {
            filter: '',
            churchId: null,
            serviceTypeId: [],
            startTime: null,
            endTime: null,
            weekDay: [],
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

    $scope.$watch('query.serviceTypeId', function (newValue, oldValue) {
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

    $scope.$watch('query.weekDay', function (newValue, oldValue) {
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
    $scope.$watch('query.startTime', function (newValue, oldValue) {
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
    $scope.$watch('query.endTime', function (newValue, oldValue) {
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

    $scope.searchChurch = function (searchText) {
        $scope.test = [];

        angular.forEach($scope.churches, function (event) {

            if (event.churchName.toLowerCase().match(searchText.toLowerCase())) {
                $scope.test.push(event);
            }

        });

        return $scope.test;
    };
    $scope.selectedChurchName = function ($item) {
        $scope.query.churchId = $item !== null && $item !== undefined ? $item.id : null;

        $scope.search();
    };

    $scope.search = function () {
        if ($scope.query.startTime !== null && $scope.query.startTime) {

            $scope.startTime = $filter('date')($scope.query.startTime, 'HH:mm:ss');
            $scope.query.startTime = $scope.startTime;
        }
        if ($scope.query.endTime !== null && $scope.query.endTime) {

            $scope.endTime = $filter('date')($scope.query.endTime, 'HH:mm:ss');
            $scope.query.endTime = $scope.endTime;
        }

        $scope.promise = churchServiceTimeService.search($scope.query).then(success);
    };
    $scope.detail = function (id) {
        if (id === undefined) {
            id = '0';
        }
        $window.location.href = '/churchServiceTime/detail/' + id;
    };

    //$scope.import = function () {
    //    $window.location.href = '/churchServiceTime/import';
    //};

    function success(resp) {
        $scope.churchServiceTimes = resp.data.result;
        $scope.count = resp.data.count;
    }
    function init() {

        churchService.getAll().then(function (resp) {
            $scope.churches = resp.data;
        });

        serviceTypeService.getAll().then(function (resp) {
            $scope.serviceTypes = resp.data;
        });

        $scope.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thurseday", "Friday", "Saturday"];

        $scope.search();
    }
    init();
});