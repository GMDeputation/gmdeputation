app.controller('stateEditController', function ($scope, $window, $location, $mdDialog, stateService, countryService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('edit/') + 5);

    $scope.backToList = function () {
        $window.location.href = '/states';
    };
    $scope.save = function () {
        stateService.edit($scope.state).then(processSuccess, processError);
    };

    function init() {
        stateService.get(id).then(function (data) {
            $scope.state = data;
        });

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
                    .textContent("State updated successfully")
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
                .textContent('Failed To Update State')
                .ok('OK')
        );
    }

    init();
});