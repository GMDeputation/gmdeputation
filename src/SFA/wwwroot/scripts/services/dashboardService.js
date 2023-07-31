app.service('dashboardService', function ($http) {

    this.getMissionarySummary = function () {
        var resp = $http.get('/dashBoard/getMissionarySummary');
        return resp;
    };

    this.getChurchEQ = function () {
        var resp = $http.get('/dashBoard/getChurchEQ');
        return resp;
    };

    this.getChurchPastorKPI = function () {
        var resp = $http.get('/dashBoard/getChurchPastorKPI');
        return resp;
    };

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