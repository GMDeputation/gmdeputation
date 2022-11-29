app.controller('statesController', function ($scope, $window, $mdDialog, stateService, countryService) {
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        filter: '',
        countryId: '0',
        limit: 20,
        order: 'name',
        page: 1
    };
    $scope.showFilter = false;
    $scope.selected = [];
    $scope.data = [];

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
    $scope.$watch('query.countryId', function (newValue, oldValue) {
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

    $scope.filter = function () {
        $scope.showFilter = true;
    };
    $scope.close = function () {
        $scope.showFilter = false;
    };
    $scope.search = function () {
        $scope.promise = stateService.search($scope.query).then(success);
    };
    $scope.add = function () {
        $window.location.href = '/states/add';
    };
    $scope.export = function () {
        $window.location.href = '/states/export';
    };
    $scope.edit = function (id) {
        $window.location.href = '/states/edit/' +id;
    };

    $scope.delete = function (id) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete the State?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            stateService.delete(id).then(processSuccess, processError);
        }, function () {
            $mdDialog.hide();
        });
    };

    //$scope.delete = function (id) {
    //    stateService.delete(id).then(processSuccess, processError);
    //};

    function processSuccess(obj) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('State Deleted successfully')
                .ariaLabel('Alert Dialog')
                .ok('OK')
        ).then(function () {
            $window.location.href = '/states';
        });
    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Delete State')
                .ok('OK')
        );
    }

    function success(resp) {
        $scope.states = resp.data.result;
        $scope.count = resp.data.count;
    }
    function init() {
        $scope.search();
        countryService.getAll().then(function (resp) {
            $scope.countries = resp.data;
        });
    }
    init();
});