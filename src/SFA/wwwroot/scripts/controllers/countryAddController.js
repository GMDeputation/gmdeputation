app.controller('countryAddController', function ($scope, $window, $location, $mdDialog, countryService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/countries';
    };
    $scope.save = function () {
        countryService.add($scope.country).then(processSuccess, processError);
    };

    function processSuccess(obj) {
        if (obj.data==="") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Country saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/countries';
            });
        }
        else
        {
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
                .textContent('Failed To Save Country')
                .ok('OK')
        );
    }
});