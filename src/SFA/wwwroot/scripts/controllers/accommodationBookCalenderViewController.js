app.controller('accommodationBookCalenderViewController', function ($scope, $window, accomodationBookService) {
    $scope.backToList = function () {
        $window.location.href = '/accomodation-booking';
    };

    function init() {
        accomodationBookService.getAll().then(function (resp) {
            $scope.accoummodations = resp.data;

            scheduler.config.xml_date = "%Y-%m-%d %h:%i";
            scheduler.init(document.getElementById('scheduler_here'), new Date(), "month");

            for (var i = 0; i < $scope.accoummodations.length; i++) {
                scheduler.parse([{
                    id: $scope.accoummodations[i].id,
                    start_date: $scope.accoummodations[i].checkinDate,
                    end_date: $scope.accoummodations[i].checkoutDate,
                    text: $scope.accoummodations[i].userName
                }], "json");
            }

            scheduler.attachEvent("onClick", function (id, ev) {
                $window.location.href = '/accomodation-booking/view/' + id;
            });
            scheduler.attachEvent("onEmptyClick", function (id, ev) {
                id = "0";
                $window.location.href = '/accomodation-booking/detail/' + id;
            });
            scheduler.attachEvent("onDblClick", function (id, ev) {
                return false;
            });
        });
    }
    init();
});