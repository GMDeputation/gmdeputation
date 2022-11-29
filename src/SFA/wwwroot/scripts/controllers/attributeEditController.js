app.controller('attributeEditController', function ($scope, $window, $location, $mdDialog, attributeService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('edit/') + 5);

    $scope.backToList = function () {
        $window.location.href = '/attribute';
    };
    $scope.save = function () {
        attributeService.edit($scope.attribute).then(processSuccess, processError);
    };


    $scope.addNewState = function () {
        $scope.attribute.attributes = $scope.attribute.attributes.concat({});
    };

    $scope.deleteState = function (index) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete Attribute?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.attribute.attributes.splice(index, 1);
            //$scope.isUpload = false;
        }, function () {
            $mdDialog.hide();
        });
    };

    function init() {
        attributeService.get(id).then(function (data) {
            $scope.attribute = data;
        });

        
    }
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Attribute updated successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/attribute';
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
                .textContent('Failed To Update Attribute')
                .ok('OK')
        );
    }

    init();
});