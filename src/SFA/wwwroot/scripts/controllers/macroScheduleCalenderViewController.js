app.controller('macroScheduleCalenderViewController', function ($scope, $window, macroScheduleService) {
    //init scheduler
    // scheduler.config.xml_date = "%Y-%m-%d %h:%i";
    //console.log('%%%%%%%%%% ' + document.getElementById('scheduler_here'));
    //scheduler.init(document.getElementById('scheduler_here'), new Date(), "month");
    //scheduler.init("scheduler_here", new Date(), "month");

    $scope.backToList = function () {
        $window.location.href = '/macroSchedule';
    };

    function init() {
        macroScheduleService.getAll().then(function (resp) {
            $scope.schedules = resp.data;

            scheduler.config.xml_date = "%Y-%m-%d %h:%i";
            scheduler.init(document.getElementById('scheduler_here'), new Date(), "month");

            for (var i = 0; i < $scope.schedules.length; i++) {
                scheduler.parse([{
                    id: $scope.schedules[i].id,
                    start_date: $scope.schedules[i].startDate,
                    end_date: $scope.schedules[i].endDate,
                    text: $scope.schedules[i].macroScheduleDesc,
                    acc: $scope.schedules[i].status,
                    accId: $scope.schedules[i].macroScheduleId
                }], "json");
            }

            scheduler.attachEvent("onClick", function (id, ev) {
                $window.location.href = '/macroSchedule/edit/' + id;
            });
            scheduler.attachEvent("onEmptyClick", function (id, ev) {

                var guidId = '0';
                $window.location.href = '/macroSchedule/detail/' + guidId;
            });
            scheduler.attachEvent("onDblClick", function (id, ev) {
                return false;
            });
        });
    }
    init();
});