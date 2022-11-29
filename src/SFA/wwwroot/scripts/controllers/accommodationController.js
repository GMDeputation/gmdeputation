app.controller('accommodationController', function ($scope, $window, $location, $mdDialog, churchService, accommodationService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);
    $scope.isDisabled = false;

    $scope.backToMain = function () {
        $window.location.href = '/home';
    };
    $scope.backToList = function () {
        $window.location.href = '/accommodations';
    };

    $scope.searchChurch = function (searchText) {  
        $scope.test = [];

        angular.forEach($scope.churches, function (event) {
           
            if (event.churchName.toLowerCase().match(searchText)) {
                $scope.test.push(event);
            }

        });

        return $scope.test;
    };
    $scope.selectedChurchName = function ($item) {
        $scope.accommodation.churchId = $item !== null && $item !== undefined ? $item.id : '0';

    };
    $scope.save = function () {
        $scope.isDisabled = true;

        accommodationService.save($scope.accommodation).then(processSuccess, processError);
    };

    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Accommodation saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $scope.isDisabled = false;
                $window.location.href = '/accommodations';
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
            ).then(function () {
                $scope.isDisabled = false;
            });
        }
    }

    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Save Accommodation')
                .ok('OK')
        ).then(function () {
            $scope.isDisabled = false;
        });
    }



    function init() {
        churchService.getAll().then(function (resp) {
            $scope.churches = resp.data;
        });
       
        if (id !== null && id !== undefined && id !== '0') {
            accommodationService.get(id).then(function (resp) {
                $scope.accommodation = resp;

                if ($scope.accommodation.churchId !== null && $scope.accommodation.churchId !== undefined && $scope.accommodation.churchId !== '0') {

                    $scope.church = { "id": $scope.accommodation.churchId, "churchName": $scope.accommodation.churchName };
                }
            });
        }
        else {
            $scope.accommodation = {};
        }
    }

    init();
});