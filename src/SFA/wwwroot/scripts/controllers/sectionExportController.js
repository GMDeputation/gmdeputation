app.controller('sectionExportController', function ($scope, $window, $location, $mdDialog, sectionService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/sections';
    };
    //$scope.save = function () {
    //    stateService.add($scope.state).then(processSuccess, processError);
    //};

    $scope.browse = function () {
        angular.element(document.getElementById('infile'))[0].click();
    };
    $scope.download = function () {
        $window.location.href = '/resources/sections/Section.xlsx';
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

        fd.append('files', $scope.file);
        //fd.append('banner[id]', $scope.banner.id);
        //fd.append('banner[displayPosition]', 'bottom');
        //fd.append('banner[caption]', $scope.banner.caption);
        //fd.append('banner[targetUrl]', $scope.banner.targetUrl === undefined ? '' : $scope.banner.targetUrl);
        sectionService.export(fd).then(processSuccess, processError);
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
                    .textContent("Sections saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/sections';
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
                .textContent('Failed To Save Section')
                .ok('OK')
        );
    }

    //init();

});