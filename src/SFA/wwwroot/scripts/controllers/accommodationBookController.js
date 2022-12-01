app.controller('accommodationBookController', function ($scope, $window, $filter, $mdDialog, $location, accomodationBookService, districtService, churchService, accommodationService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('detail/') + 7);
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
        $scope.accommodationBook.districtId = $item !== null && $item !== undefined ? $item.id : '00000000-0000-0000-0000-000000000000';
        $scope.churches = [];

        if ($scope.accommodationBook.districtId !== null && $scope.accommodationBook.districtId !== undefined && $scope.accommodationBook.districtId !== '00000000-0000-0000-0000-000000000000') {

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
        $scope.accommodationBook.churchId = $item !== null && $item !== undefined ? $item.id : '00000000-0000-0000-0000-000000000000';

        if ($scope.accommodationBook.churchId !== null && $scope.accommodationBook.churchId !== undefined && $scope.accommodationBook.churchId !== '00000000-0000-0000-0000-000000000000') {

            accommodationService.getAllByChurch($scope.accommodationBook.churchId).then(function (resp) {
                $scope.accommodations = resp;
            });
        }
    };


    $scope.save = function () {
        $scope.isDisabled = true;
        
        if ($scope.accommodationBook.districtId === null && $scope.accommodationBook.districtId === undefined && $scope.accommodationBook.districtId === '00000000-0000-0000-0000-000000000000') {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Search And Select correct District')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $scope.isDisabled = false;
            });
            return false;
        }
        else if ($scope.accommodationBook.churchId === null && $scope.accommodationBook.churchId === undefined && $scope.accommodationBook.churchId === '00000000-0000-0000-0000-000000000000') {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Search And Select correct Curch')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $scope.isDisabled = false;
            });
            return false;
        }
        else {
            $scope.arrivalTime = $filter('date')($scope.accommodationBook.arrivalTime, 'HH:mm:ss');
            $scope.accommodationBook.arrivalTime = $scope.arrivalTime;

            $scope.departureTime = $filter('date')($scope.accommodationBook.departureTime, 'HH:mm:ss');
            $scope.accommodationBook.departureTime = $scope.departureTime;

            accomodationBookService.save($scope.accommodationBook).then(function (resp) {
                if (resp.data !== null && resp.data !== undefined && resp.data === 1) {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Accommodation Booked Saved successfully')
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $window.location.href = '/accomodation-booking';
                        $scope.isDisabled = false;
                    });
                }
                else if (resp.data !== null && resp.data !== undefined && resp.data === -1) {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('On this date range already a Accommodation Booked')
                            .ariaLabel('Alert Dialog')
                            .ok('OK')
                    ).then(function () {
                        $scope.isDisabled = false;
                    });
                }
                else {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Failed To Saved Accommodation Booked')
                            .ok('OK')
                    ).then(function () {
                        $scope.isDisabled = false;
                    });
                }
            });
        }
    };

    $scope.submit = function () {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Submit?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.isDisabled = true;

            $scope.arrivalTime = $filter('date')($scope.accommodationBook.arrivalTime, 'HH:mm:ss');
            $scope.accommodationBook.arrivalTime = $scope.arrivalTime;

            $scope.departureTime = $filter('date')($scope.accommodationBook.departureTime, 'HH:mm:ss');
            $scope.accommodationBook.departureTime = $scope.departureTime;
            
            accomodationBookService.submit($scope.accommodationBook).then(function (resp) {
                if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Accommodation Booked Submit successfully')
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
                            .textContent('Failed To Submit Accommodation Booked')
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
        $scope.minDate = new Date();
        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });

        if (id !== null && id !== undefined && id !== '00000000-0000-0000-0000-000000000000') {
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