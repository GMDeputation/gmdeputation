app.controller('churchesController', function ($scope, $window, $mdDialog, churchService, sectionService, districtService) {
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        filter: '',
        email: '',
        sectionId: '0',
        districtId: '0',
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
        if ($scope.query.sectionId === undefined || $scope.query.sectionId === null) {
            $scope.query.sectionId = '0';
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });
    $scope.$watch('query.email', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue !== oldValue) {
            $scope.query.page = 1;
        }
        if ($scope.query.sectionId === undefined || $scope.query.sectionId === null) {
            $scope.query.sectionId = '0';
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });

    $scope.$watch('query.districtId', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue === "") {
            newValue = '0';
            $scope.query.districtId = '0';
        }
        if (newValue !== oldValue) {
            $scope.query.page = 1;
            if (oldValue !== undefined) {
                $scope.query.sectionId = '0';
            }
            sectionService.getSectionByDistrictId(newValue).then(function (resp) {
                $scope.sections = resp;
            });
            $scope.search();
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });

    $scope.$watch('query.sectionId', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue === "") {
            newValue = '0';
            $scope.query.sectionId = '0';
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
        $scope.promise = churchService.search($scope.query).then(success);
    };
    $scope.add = function () {
        $window.location.href = '/churches/add';
    };
    $scope.export = function () {
        $window.location.href = '/churches/export';
    };
    $scope.viewOnMap = function () {
        $window.location.href = '/churches/mapView';
    };
    $scope.edit = function (id) {
        $window.location.href = '/churches/edit/' + id;
    };

    $scope.delete = function (id) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete the Church?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            churchService.delete(id).then(processSuccess, processError);
        }, function () {
            $mdDialog.hide();
        });
    };

    //$scope.delete = function (id) {
    //    churchService.delete(id).then(processSuccess, processError);
    //};

    function processSuccess(obj) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Church Deleted successfully')
                .ariaLabel('Alert Dialog')
                .ok('OK')
        ).then(function () {
            $window.location.href = '/churches';
        });
    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Delete Church')
                .ok('OK')
        );
    }

    function success(resp) {
        $scope.churchs = resp.data.result;
        $scope.count = resp.data.count;
    }
    function init() {
        $scope.search();
        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });
    }

    init();
});