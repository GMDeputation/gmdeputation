app.controller('accommodationBookFeedbackController', function ($scope, $window, $mdDialog, $location, accomodationBookService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('feedBack/') + 9);
    $scope.accommodationBook = {};
    $scope.backToList = function () {
        $window.location.href = '/accomodation-booking';
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

            accomodationBookService.submitFeedBack($scope.accommodationBook).then(function (resp) {
                if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .clickOutsideToClose(false)
                            .title('Church Admin')
                            .textContent('Accommodation Booked FeedBack Submit successfully')
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
                            .textContent('Failed To Submit Accommodation Booked FeedBack')
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
        if (id !== null && id !== undefined && id !== '0') {
            accomodationBookService.get(id).then(function (resp) {
                $scope.accommodationBook = resp;               
            });
        }
    }
    init();
});