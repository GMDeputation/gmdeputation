app.controller('countryEditController', function ($scope, $window, $location, $mdDialog, countryService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('edit/') + 5);

    $scope.backToList = function () {
        $window.location.href = '/countries';
    };
    $scope.save = function () {
        countryService.edit($scope.country).then(processSuccess, processError);
    };

    function init() {
        countryService.get(id).then(function (data) {
            $scope.country = data;
        });

        
    }
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Country updated successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/countries';
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
                .textContent('Failed To Update Country')
                .ok('OK')
        );
    }

    init();
});