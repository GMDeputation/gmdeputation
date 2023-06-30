app.service('dashboardService', function ($http) {
    this.getCount = function () {
        return $http.get('/dashBoard/getCount');
    };
});