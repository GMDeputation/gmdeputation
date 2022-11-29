app.controller('districtEditController', function ($scope, $window, $location, $mdDialog, districtService, stateService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('edit/') + 5);

    $scope.backToList = function () {
        $window.location.href = '/districts';
    };
    $scope.save = function () {
        districtService.edit($scope.district).then(processSuccess, processError);
    };

    //$scope.$watch('district.countryId', function (newValue, oldValue) {
    //    if (newValue !== oldValue) {
    //        if (oldValue !== undefined) {
    //            $scope.district.stateId = '0';
    //        }
    //        if (newValue !== null) {
    //            stateService.getStateByCountryId(newValue).then(function (resp) {
    //                $scope.states = resp;
    //                $scope.textDisabled2 = false;
    //            });
    //        }
    //    }
    //});

    $scope.searchState = function (searchText) {
        $scope.test = [];

        angular.forEach($scope.states, function (event) {
            $scope.eventStatus = true;
            angular.forEach($scope.district.states, function (detail) {
                if (event.id === detail.id && detail.id !== undefined) {
                    $scope.eventStatus = false;
                }
            });
            if ($scope.eventStatus === true && event.name.toLowerCase().match(searchText.toLowerCase())) {
                $scope.test.push(event);
            }

        });

        return $scope.test;
    };
    $scope.selectedStateName = function ($item, index) {
        $scope.district.states[index].id = $item.id;

    };

    $scope.addNewState = function () {
        $scope.district.states = $scope.district.states.concat({});
    };

    $scope.deleteState = function (index) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete State?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.district.states.splice(index, 1);
            //$scope.isUpload = false;
        }, function () {
            $mdDialog.hide();
        });
    };

    function init() {
 
        districtService.get(id).then(function (data) {
            $scope.district = data;
            angular.forEach($scope.district.states, function (detail) {
                //detail.$item = detail;
                detail.state = { "id": detail.id, "name": detail.name };
            });
            
            $scope.district.states = $scope.district.states.concat({});
            $scope.district.states.splice(1, 1);
            //$scope.district.countryId = data.countryId;
            //$scope.district.stateId = data.stateId;
        });

        $scope.district = {};
        $scope.district.states = [{}];
        stateService.getAll().then(function (resp) {
            $scope.states = resp.data;
        });

    }
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("District updated successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/districts';
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
                .textContent('Failed To Update District')
                .ok('OK')
        );
    }

    init();
});