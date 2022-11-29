app.controller('churchAddController', function ($scope, $window, $location, $mdDialog, churchService, sectionService, districtService) {
    $scope.file = {};

    $scope.backToList = function () {
        $window.location.href = '/churches';
    };
    $scope.save = function () {
        churchService.add($scope.church).then(processSuccess, processError);
    };

    $scope.searchDistrict = function (searchText) {
        $scope.district = [];

        if (searchText !== null && searchText !== undefined && searchText !== "") {
            searchText = searchText.toLowerCase();
        }

        angular.forEach($scope.districts, function (event) {
            if (event.name.toLowerCase().match(searchText)) {
                $scope.district.push(event);
            }
        });

        return $scope.district;
    };
    $scope.selectedDistrictName = function ($item) {
        $scope.church.districtId = $item !== null && $item !== undefined ? $item.id : '0';
        $scope.sections = [];

        if ($scope.church.districtId !== null && $scope.church.districtId !== undefined && $scope.church.districtId !== '0') {

            sectionService.getSectionByDistrictId($scope.church.districtId).then(function (resp) {
                $scope.sections = resp;
            });
        }
    };

    //$scope.$watch('church.sectionId', function (newValue, oldValue) {
    //    if (newValue !== oldValue) {
    //        //if (oldValue !== undefined) {
    //        //    $scope.church.stateId = '0';
    //        //}
    //        if (newValue !== null) {
    //            sectionService.getStateByCountryId(newValue).then(function (resp) {
    //                $scope.states = resp;
    //                $scope.textDisabled2 = false;
    //            });
    //        }
    //    }
    //});
    function init() {        
        //sectionService.getAll().then(function (resp) {
        //    $scope.sections = resp.data;
        //});
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
                    .textContent("Church saved successfully")
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

    init();

});