app.controller('appointmentCalenderViewController', function ($scope, $window, appointmentService) {
    //YYYY-MM-DD HH:mm
    //var date = "2017-03-13";
    //var time = "18:00";
    //var timeAndDate = moment(date + ' ' + time);
    //console.log(timeAndDate);

    $scope.backToList = function () {
        $window.location.href = '/appointments';
    };

    function init() {
        appointmentService.getAll().then(function (resp) {
            $scope.appointments = resp.data;

            scheduler.config.xml_date = "%Y-%m-%d %h:%i";
            scheduler.init(document.getElementById('scheduler_here'), new Date(), "month");

            for (var i = 0; i < $scope.appointments.length; i++) {
                scheduler.parse([{
                    id: $scope.appointments[i].id,
                    start_date: $scope.appointments[i].eventDate,
                    end_date: $scope.appointments[i].eventDate,
                    text: $scope.appointments[i].description,
                    acc: $scope.appointments[i].status,
                    accId: $scope.appointments[i].macroScheduleDetailId
                }], "json");
            }

            scheduler.attachEvent("onClick", function (id, ev) {
                $window.location.href = '/appointments/detail/' + id;
            });
            scheduler.attachEvent("onEmptyClick", function (id, ev) {
                $window.location.href = '/macroSchedule/approvedSchedule';
            });
            scheduler.attachEvent("onDblClick", function (id, ev) {
                return false;
            });
        });
    }
    init();
});