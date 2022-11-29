app.controller('serviceTypesController', function ($scope, $window, $mdDialog, serviceTypeService) {
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        name: '',
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

    $scope.search = function () {
        $scope.promise = serviceTypeService.search($scope.query).then(success);
    };
    $scope.detail = function (id) {
        if (id === undefined) {
            id = '0';
        }
        $window.location.href = '/serviceType/detail/' + id;
    };

    $scope.delete = function (id) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete the Service Type?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            serviceTypeService.delete(id).then(processSuccess, processError);
        }, function () {
            $mdDialog.hide();
        });
    };
    //$scope.import = function () {
    //    $window.location.href = '/serviceType/import';
    //};

    function processSuccess(obj) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Service Type Deleted successfully')
                .ariaLabel('Alert Dialog')
                .ok('OK')
        ).then(function () {
            $window.location.href = '/serviceType';
        });
    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Delete Service Type')
                .ok('OK')
        );
    }

    function success(resp) {
        $scope.serviceTypes = resp.data.result;
        $scope.count = resp.data.count;
    }
    function init() {
        $scope.search();
    }
    init();
});