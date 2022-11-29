app.controller('churchServiceTimeDetailController', function ($scope, $filter, $window, $location, $mdDialog, serviceTypeService, churchServiceTimeService, churchService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);
   
    $scope.backToList = function () {
        $window.location.href = '/churchServiceTime';
    };

    $scope.churchServiceTime = {
        serviceTime: new Date()
    };

    $scope.message = {
        hour: 'Hour is required',
        minute: 'Minute is required',
        meridiem: 'Meridiem is required'
    };

    $scope.save = function () {
        $scope.time = $filter('date')($scope.churchServiceTime.serviceTime, 'HH:mm:ss');
        $scope.churchServiceTime.serviceTime = $scope.time;

        churchServiceTimeService.save($scope.churchServiceTime).then(processSuccess, processError);
    };

    $scope.searchChurch = function (searchText) {
        $scope.test = [];

        angular.forEach($scope.churches, function (event) {

            if (event.churchName.toLowerCase().match(searchText.toLowerCase())) {
                $scope.test.push(event);
            }

        });

        return $scope.test;
    };
    $scope.selectedChurchName = function ($item) {
        $scope.churchServiceTime.churchId = $item !== null && $item !== undefined ? $item.id : '0';

    };
    function init() {
        churchService.getAll().then(function (resp) {
            $scope.churches = resp.data;
        });

        serviceTypeService.getAll().then(function (resp) {
            $scope.serviceTypes = resp.data;
        });

        $scope.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thurseday", "Friday", "Saturday"];

        $scope.churchServiceTime = {};
        if (id !== null && id !== undefined && id !== '0') {
            churchServiceTimeService.get(id).then(function (resp) {
                $scope.churchServiceTime = resp;

                if (resp.serviceTime !== null) {
                    $scope.serviceTime = resp.serviceTime;
                    $scope.churchServiceTime.serviceTime = new Date();
                    $scope.time = moment($scope.serviceTime, 'HH:mm');
                    $scope.churchServiceTime.serviceTime.setTime($scope.time);
                }
                else {
                    $scope.churchServiceTime.serviceTime = new Date();
                }

                if ($scope.churchServiceTime.churchId !== null && $scope.churchServiceTime.churchId !== undefined && $scope.churchServiceTime.churchId !== '0') {

                    $scope.church = { "id": $scope.churchServiceTime.churchId, "churchName": $scope.churchServiceTime.churchName };
                }
            });
        }
        else {
            $scope.churchServiceTime.serviceTime = new Date();
        }
    }
    init();
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Church Service Time saved successfully")
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
                .textContent('Failed To Save Church Service Time')
                .ok('OK')
        );
    }
});