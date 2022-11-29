app.controller('stateAddController', function ($scope, $window, $location, $mdDialog, stateService, countryService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/states';
    };
    $scope.save = function () {
        stateService.add($scope.state).then(processSuccess, processError);
    };

    function init() {
        countryService.getAll().then(function (resp) {
            $scope.countries = resp.data;
        });

    }

    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("State saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/states';
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
                .textContent('Failed To Save State')
                .ok('OK')
        );
    }

    init();

});