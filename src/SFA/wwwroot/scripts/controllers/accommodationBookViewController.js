app.controller('accommodationBookViewController', function ($scope, $window, $filter, $mdDialog, $location, accomodationBookService, districtService, churchService, accommodationService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('view/') + 5);
    $scope.accommodationBook = {};
    $scope.backToList = function () {
        $window.location.href = '/accomodation-booking';
    };
    $scope.accommodationBook = {
        arrivalTime: new Date(),
        departureTime: new Date()
    };

    $scope.message = {
        hour: 'Hour is required',
        minute: 'Minute is required',
        meridiem: 'Meridiem is required'
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
        $scope.accommodationBook.districtId = $item !== null && $item !== undefined ? $item.id : '0';
        $scope.churches = [];

        if ($scope.accommodationBook.districtId !== null && $scope.accommodationBook.districtId !== undefined && $scope.accommodationBook.districtId !== '0') {

            churchService.getChurchByDistrict($scope.accommodationBook.districtId).then(function (resp) {
                $scope.churches = resp;
            });          
        }
    };


    $scope.searchChurch = function (searchText) {
        $scope.test = [];

        if (searchText !== null && searchText !== undefined && searchText !== "") {
            searchText = searchText.toLowerCase();
        }
        angular.forEach($scope.churches, function (event) {
            if (event.churchName.toLowerCase().match(searchText)) {
                $scope.test.push(event);
            }
        });

        return $scope.test;
    };
    $scope.selectedChurchName = function ($item) {
        $scope.accommodationBook.churchId = $item !== null && $item !== undefined ? $item.id : '0';

        if ($scope.accommodationBook.churchId !== null && $scope.accommodationBook.churchId !== undefined && $scope.accommodationBook.churchId !== '0') {

            accommodationService.getAllByChurch($scope.accommodationBook.churchId).then(function (resp) {
                $scope.accommodations = resp;
            });
        }
    };


    $scope.approved = function () {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Approved?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.isDisabled = true;

            $scope.arrivalTime = $filter('date')($scope.accommodationBook.arrivalTime, 'HH:mm:ss');
            $scope.accommodationBook.arrivalTime = $scope.arrivalTime;

            $scope.departureTime = $filter('date')($scope.accommodationBook.departureTime, 'HH:mm:ss');
            $scope.accommodationBook.departureTime = $scope.departureTime;

            accomodationBookService.approved($scope.accommodationBook).then(function (resp) {
                if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Accommodation Booked Approved successfully')
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $window.location.href = '/accomodation-booking';
                        $scope.isDisabled = false;
                    });
                }
                else {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Failed To Approved Accommodation Booked')
                            .ok('OK')
                    ).then(function () {
                        $scope.isDisabled = false;
                    });
                }
            });
        }, function () {
            $mdDialog.hide();
        });
    };

    function init() {
        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });

        if (id !== null && id !== undefined && id !== '0') {
            accomodationBookService.get(id).then(function (resp) {
                $scope.accommodationBook = resp; 

                $scope.accommodationBook.district = { "id": $scope.accommodationBook.districtId, "name": $scope.accommodationBook.districtName };
                $scope.accommodationBook.church = { "id": $scope.accommodationBook.churchId, "churchName": $scope.accommodationBook.churchName };

                if (resp.arrivalTime !== null && resp.departureTime !== null) {
                    $scope.arrivalTime = resp.arrivalTime;
                    $scope.accommodationBook.arrivalTime = new Date();
                    $scope.time1 = moment($scope.arrivalTime, 'HH:mm');
                    $scope.accommodationBook.arrivalTime.setTime($scope.time1);

                    $scope.departureTime = resp.departureTime;
                    $scope.accommodationBook.departureTime = new Date();
                    $scope.time2 = moment($scope.departureTime, 'HH:mm');
                    $scope.accommodationBook.departureTime.setTime($scope.time2);
                }
                else {
                    $scope.accommodationBook.arrivalTime = new Date();
                    $scope.accommodationBook.departureTime = new Date();
                }
            });
        }
    }
    init();
});