app.controller('roleController', function ($scope, $window, $location, $mdDialog, roleService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);
    $scope.role = {};
    $scope.isDisabled = false;

    $scope.backToMain = function () {
        $window.location.href = '/home';
    };
    $scope.backToList = function () {
        $window.location.href = '/role';
    };

    $scope.save = function () {
        $scope.isDisabled = true;

        roleService.save($scope.role).then(function (resp) {
            if (resp.data !== null && resp.data !== undefined && resp.data === true) {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Role Saved successfully')
                        .ariaLabel('Alert Dialog')
                        .ok('OK')
                ).then(function () {
                    $window.location.href = '/role';
                    $scope.isDisabled = false;
                });
            }
            else {
                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(false)
                        .title('Church Admin')
                        .textContent('Failed To Saved Role')
                        .ok('OK')
                ).then(function () {
                    $scope.isDisabled = false;
                });
            }
        });
    };

    function init() {
        if (id !== null && id !== undefined && id !== '0') {
            roleService.get(id).then(function (resp) {
                $scope.role = resp;
            });
        }
    }

    init();
});