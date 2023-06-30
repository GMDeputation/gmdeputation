app.service('dashboardService', function ($http) {

    this.getCount = function () {
        var resp = $http.get('/dashBoard/getCount');
        return resp;
    };

    this.getServiceOneYearCount = function () {
        var resp = $http.get('/dashBoard/getServiceOneYearCount');
        return resp;
    };

    this.getMacroScheduleThirtyDayCount = function () {
        var resp = $http.get('/dashBoard/getMacroScheduleThirtyDayCount');
        return resp;
    };

});