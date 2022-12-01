app.controller('accommodationBookingsController', function ($scope, $window, $mdDialog, accomodationBookService) {
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        searchText: '',
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
            searchText: '',
            fromDate: null,
            toDate: null,
            limit: 20,
            order: 'name',
            page: 1
        };
        $scope.search();
    };


    $scope.$watch('query.searchText', function (newValue, oldValue) {
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
        $scope.promise = accomodationBookService.search($scope.query).then(success);
    };   
    $scope.detail = function (id) {
        if (id === undefined) {
            id = '0';
        }
        $window.location.href = '/accomodation-booking/detail/' + id;
    };
    $scope.view = function (id) {       
        $window.location.href = '/accomodation-booking/view/' + id;
    };
    $scope.feedBack = function (id) {       
        $window.location.href = '/accomodation-booking/feedBack/' + id;
    };
    $scope.viewOnCalender = function () {
        $window.location.href = '/accomodation-booking/calender';
    };
    function success(resp) {
        $scope.accommodationBookings = resp.data.result;
        $scope.count = resp.data.count;
    }

    function init() {
        $scope.search();
    }
    init();
});