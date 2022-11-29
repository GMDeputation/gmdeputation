app.controller('attributeDetailController', function ($scope, $window, $location, $mdDialog, attributeService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('attributeDetail/') + 16);

    $scope.backToList = function () {
        $window.location.href = '/attribute';
    };
    $scope.save = function () {
        attributeService.saveAttribute($scope.attribute).then(processSuccess, processError);
    };

    function init() {
        $scope.attribute = {};

        attributeService.getAll().then(function (resp) {
            $scope.attributes = resp.data;
        });

        if (id !== null && id !== undefined && id !== '0') {
            attributeService.getByAttribute(id).then(function (resp) {
                $scope.attribute = resp;
            });
        }
    }
    init();
    function processSuccess(obj) {
        if (obj.data==="") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Attribute saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/attribute';
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
                .textContent('Failed To Save Attribute')
                .ok('OK')
        );
    }
});