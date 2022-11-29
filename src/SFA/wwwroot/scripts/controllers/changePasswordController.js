app.controller('changePasswordController', function ($scope, $window, $location, $mdDialog, userService) {

    $scope.backToList = function () {
        $window.location.href = '/nav/Security';
    };

    $scope.changePass = function () {

        if ($scope.user.newPassword !== $scope.user.confirmPassword) {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Password Not Match')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $scope.user.newPassword = null;
                $scope.user.confirmPassword = null;
                $mdDialog.hide();
            });
        }
        else {
            userService.changePass($scope.user).then(function (resp) {
                $scope.users = resp.data;

                if ($scope.users === false) {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Your Current Password Does Not Match')
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $scope.user.password = null;
                        $mdDialog.hide();
                    });
                }
                else {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Password Change successfully')
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $window.location.href = '/home';
                    });
                }
            });
        }
    };

    function init() {
    }

    init();

});