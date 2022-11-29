app.controller('attributeImportController', function ($scope, $window, $location, $mdDialog, attributeService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/attribute';
    };

    $scope.browse = function () {
        angular.element(document.getElementById('infile'))[0].click();
    };
    $scope.download = function () {
        $window.location.href = '/resources/attributes/Attribute.xlsx';
    };
    $scope.setFileName = function (element) {
        $scope.file = element.files[0];
        $scope.fileName = element.files[0].name;
        var fileSize = (30 * 1024 * 1024);
        if ($scope.file.size > fileSize) {
            $scope.fileName = null;
            $scope.file = null;

            alert("File is bigger than 30 MB");
        }
    };

    $scope.save = function () {
        var fd = new FormData();

        fd.append('files', $scope.file);
        attributeService.import(fd).then(processSuccess, processError);
    };

    function processSuccess(obj) {
        if (obj.data === "") {
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
                .textContent('Failed To Save Attribute')
                .ok('OK')
        );
    }

});