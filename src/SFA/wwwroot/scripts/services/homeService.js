app.service('homeService', function ($http) {
    this.getDashbordData = function () {
        return $http.get('/home/api/getDashbordData');
    };
    this.getDistrictThisMonth = function () {
        return $http.get('/home/api/getDistrictThisMonth');
    };
    this.getDistrictNextMonth = function () {
        return $http.get('/home/api/getDistrictNextMonth');
    };
    this.getSectionThisMonth = function () {
        return $http.get('/home/api/getSectionThisMonth');
    };
    this.getSectionNextMonth = function () {
        return $http.get('/home/api/getSectionNextMonth');
    };
});