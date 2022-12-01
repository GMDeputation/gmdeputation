app.controller('churchServiceTimeImportController', function ($scope, $window, $location, $mdDialog, churchServiceTimeService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/churchServiceTime';
    };

    $scope.browse = function () {
        angular.element(document.getElementById('infile'))[0].click();
    };
    $scope.download = function () {
        $window.location.href = '/resources/churchServiceTime/churchServiceTime.xlsx';
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
    $scope.removeFile = function () {
        $scope.fileName = '';
        $scope.state.fileName = undefined;
    };
    $scope.save = function () {
        var fd = new FormData();

        fd.append('file', $scope.file);
        churchServiceTimeService.import(fd).then(processSuccess, processError);
    };

    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Churchs Service Time saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/churchServiceTime';
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
                .textContent('Failed To Save Service Time')
                .ok('OK')
        );
    }
    
});