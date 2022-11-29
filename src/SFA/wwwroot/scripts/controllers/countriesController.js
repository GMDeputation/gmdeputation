app.controller('countriesController', function ($scope, $window, $mdDialog, countryService) {
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        filter: '',
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

    $scope.filter = function () {
        $scope.showFilter = true;
    };
    $scope.close = function () {
        $scope.showFilter = false;
    };
    $scope.search = function () {
        $scope.promise = countryService.search($scope.query).then(success);
    };
    $scope.export = function () {
        $window.location.href = '/countries/export';
    };
    $scope.add = function () {
        $window.location.href = '/countries/add';
    };
    $scope.edit = function (id) {
        $window.location.href = '/countries/edit/' + id;
    };

    $scope.delete = function (id) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete the Country?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            countryService.delete(id).then(processSuccess, processError);
        }, function () {
            $mdDialog.hide();
        });
    };


    //$scope.delete = function (id) {
    //    countryService.delete(id).then(processSuccess, processError);
    //};

    function processSuccess(obj) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Country Deleted successfully')
                .ariaLabel('Alert Dialog')
                .ok('OK')
        ).then(function () {
            $window.location.href = '/countries';
        });
    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Delete Country')
                .ok('OK')
        );
    }

    function success(resp) {
        $scope.countries = resp.data.result;
        $scope.count = resp.data.count;
    }
    function init() {
        $scope.search();
    }
    init();
});