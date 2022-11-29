app.controller('serviceTypeDetailController', function ($scope, $window, $location, $mdDialog, serviceTypeService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);

    $scope.backToList = function () {
        $window.location.href = '/serviceType';
    };
    $scope.save = function () {
        serviceTypeService.save($scope.serviceType).then(processSuccess, processError);
    };

    function init() {
        $scope.serviceType = {};
        if (id !== null && id !== undefined && id !== '0') {
            serviceTypeService.get(id).then(function (resp) {
                $scope.serviceType = resp;
            });
        }
    }
    init();
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Service Type saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/serviceType';
            });
        }
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent(obj.data)
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            );
        }

    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Save Service Type')
                .ok('OK')
        );
    }
});