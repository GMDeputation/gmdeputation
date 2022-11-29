app.controller('menuController', function ($scope, $window, $location, $mdDialog, menuGroupService, menuService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);
    $scope.menu = {};
    $scope.isDisabled = false;

    $scope.backToMain = function () {
        $window.location.href = '/home';
    };
    $scope.backToList = function () {
        $window.location.href = '/menu';
    };

    $scope.save = function () {
        $scope.isDisabled = true;

        menuService.save($scope.menu).then(function (resp) {
            if (resp.data !== null && resp.data !== undefined && resp.data === true) {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Menu Saved successfully')
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $window.location.href = '/menu';
                    $scope.isDisabled = false;
                });
            }
            else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Failed To Saved Menu')
                        .ok('OK')
                ).then(function () {
                    $scope.isDisabled = false;
                });
            }
        });
    };

    $scope.$watch('menu.category', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            menuGroupService.getByCategory($scope.menu.category).then(function (resp) {
                $scope.groups = resp;
            });
        }
    });

    function init() {
        menuGroupService.allCategory().then(function (resp) {
            $scope.categories = resp.data;
        });
        if (id !== null && id !== undefined && id !== '0') {
            menuService.get(id).then(function (resp) {
                $scope.menu = resp;
            });
        }
    }

    init();
});