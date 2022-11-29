app.controller('churchEditController', function ($scope, $window, $location, $mdDialog, churchService, sectionService, districtService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('edit/') + 5);

    $scope.backToList = function () {
        $window.location.href = '/churches';
    };
    $scope.save = function () {
        churchService.edit($scope.church).then(processSuccess, processError);
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

    function init() {
        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });

        churchService.get(id).then(function (data) {
            $scope.church = data;
            $scope.church.sectionId = data.sectionId;

            $scope.district = { "id": $scope.church.districtId, "name": $scope.church.districtName };
        });        
        //sectionService.getAll().then(function (resp) {
        //    $scope.sections = resp.data;
        //});
    }
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Church updated successfully")
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
                .textContent('Failed To Update Church')
                .ok('OK')
        );
    }

    init();
});