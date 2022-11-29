app.controller('rolesController', function ($scope, $window, $mdDialog, roleService) {
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
        $scope.promise = roleService.search($scope.query).then(success);
    };
   
    $scope.detail = function (id) {
        if (id === undefined) {
            id = '0';
        }
        $window.location.href = '/role/detail/' + id;
    };

    function success(resp) {
        $scope.roles = resp.data.result;
        $scope.count = resp.data.count;
    }
    function init() {
        $scope.search();
    }
    init();
});
