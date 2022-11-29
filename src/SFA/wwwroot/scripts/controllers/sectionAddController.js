app.controller('sectionAddController', function ($scope, $window, $location, $mdDialog, sectionService, districtService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/sections';
    };
    $scope.save = function () {
        sectionService.add($scope.section).then(processSuccess, processError);
    };

    //$scope.$watch('section.countryId', function (newValue, oldValue) {
    //    if (newValue !== oldValue) {
    //        if (oldValue !== undefined) {
    //            $scope.section.stateId = '0';
    //        }
    //        if (newValue !== null) {
    //            stateService.getStateByCountryId(newValue).then(function (resp) {
    //                $scope.states = resp;
    //                $scope.textDisabled2 = false;
    //            });
    //        }
    //    }
    //});
    //$scope.$watch('section.stateId', function (newValue, oldValue) {
    //    if (newValue !== oldValue) {
    //        if (oldValue !== undefined) {
    //            $scope.section.districtId = '0';
    //        }
    //        if (newValue !== null) {
    //            districtService.getDistrictByStateId(newValue).then(function (resp) {
    //                $scope.districts = resp;
    //                $scope.textDisabled2 = false;
    //            });
    //        }
    //    }
    //});
    function init() {        
        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });
    }

    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Section saved successfully")
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

    init();

});