app.controller('attributesController', function ($scope, $window, $mdDialog, attributeService) {
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
            name: '',
            limit: 20,
            order: 'name',
            page: 1
        };
        $scope.search();
    };


    $scope.$watch('query.name', function (newValue, oldValue) {
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
        $scope.promise = attributeService.search($scope.query).then(success);
    };
    $scope.addAttributeType = function () {
        $window.location.href = '/attribute/addAttributeTypes';
    };
    $scope.addAttribute = function (id) {
        if (id === undefined) {
            id = '0';
        }
        $window.location.href = '/attribute/attributeDetail/' + id;
    };

    $scope.editAttribute = function (id) {
        $window.location.href = '/attribute/attributeDetail/' + id;
    }; 
    $scope.import = function () {
        $window.location.href = '/attribute/import';
    };
    //$scope.edit = function (id) {
    //    $window.location.href = '/attribute/edit/' + id;
    //};

    //$scope.delete = function (id) {
    //    countryService.delete(id).then(processSuccess, processError);
    //};

    //function processSuccess(obj) {
    //    $mdDialog.show(
    //        $mdDialog.alert()
    //            .clickOutsideToClose(false)
    //            .title('Church Admin')
    //            .textContent('Country Deleted successfully')
    //            .ariaLabel('Alert Dialog')
    //            .ok('OK')
    //    ).then(function () {
    //        $window.location.href = '/countries';
    //    });
    //}
    //function processError(error) {
    //    $mdDialog.show(
    //        $mdDialog.alert()
    //            .clickOutsideToClose(false)
    //            .title('Church Admin')
    //            .textContent('Failed To Delete Country')
    //            .ok('OK')
    //    );
    //}

    function success(resp) {
        $scope.attributes = resp.data.result;
        $scope.count = resp.data.count;
    }
    function init() {
        $scope.search();
    }
    init();
});