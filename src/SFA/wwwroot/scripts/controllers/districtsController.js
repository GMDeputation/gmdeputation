app.controller('districtsController', function ($scope, $window, $mdDialog, districtService, countryService, stateService) {
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        filter: '',
        countryId: '0',
        stateId: '0',
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
        if ($scope.query.stateId === undefined || $scope.query.stateId === null) {
            $scope.query.stateId = '0';
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
        if (newValue === "") {
            newValue = '0';
            $scope.query.countryId = '0';
        }
        if (newValue !== oldValue) {
            $scope.query.page = 1;
            if (oldValue !== undefined) {
                $scope.query.stateId = '0';
            }
            stateService.getStateByCountryId(newValue).then(function (resp) {
                $scope.states = resp;
            });
            $scope.search();
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });
    $scope.$watch('query.stateId', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue === "") {
            newValue = '0';
            $scope.query.stateId = '0';
        }
        if (newValue !== oldValue) {
            $scope.search();
            $scope.query.page = 1;
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

    });


    $scope.filter = function () {
        $scope.showFilter = true;
    };
    $scope.close = function () {
        $scope.showFilter = false;
    };
    $scope.search = function () {
        $scope.promise = districtService.search($scope.query).then(success);
    };
    $scope.add = function () {
        $window.location.href = '/districts/add';
    };
    $scope.export = function () {
        $window.location.href = '/districts/export';
    };
    $scope.edit = function (id) {
        $window.location.href = '/districts/edit/' + id;
    };
    $scope.delete = function (id) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete the District?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            districtService.delete(id).then(processSuccess, processError);
        }, function () {
            $mdDialog.hide();
        });
    };

    //$scope.delete = function (id) {
    //    districtService.delete(id).then(processSuccess, processError);
    //};

    function processSuccess(obj) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('District Deleted successfully')
                .ariaLabel('Alert Dialog')
                .ok('OK')
        ).then(function () {
            $window.location.href = '/districts';
        });
    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Delete District')
                .ok('OK')
        );
    }

    function success(resp) {
        $scope.districts = resp.data.result;
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