app.controller('churchExportController', function ($scope, $window, $location, $mdDialog, churchService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/churches';
    };
    //$scope.save = function () {
    //    stateService.add($scope.state).then(processSuccess, processError);
    //};

    $scope.browse = function () {
        angular.element(document.getElementById('infile'))[0].click();
    };
    $scope.download = function () {
        $window.location.href = '/resources/churchs/church.xlsx';
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
        //$rootScope.$broadcast('showProcess', 'Uploading & Saving Bottom Banner ....');
        var fd = new FormData();

        fd.append('file', $scope.file);
        //fd.append('banner[id]', $scope.banner.id);
        //fd.append('banner[displayPosition]', 'bottom');
        //fd.append('banner[caption]', $scope.banner.caption);
        //fd.append('banner[targetUrl]', $scope.banner.targetUrl === undefined ? '' : $scope.banner.targetUrl);
        churchService.export(fd).then(processSuccess, processError);
    };

    //function init() {
    //    countryService.getAll().then(function (resp) {
    //        $scope.countries = resp.data;
    //    });

    //}

    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Churchs saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/churches';
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
                .textContent('Failed To Save Church')
                .ok('OK')
        );
    }

    //init();

});