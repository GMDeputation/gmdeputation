app.service('districtService', function ($http) {
    this.getAll = function () {
        return $http.get('/districts/all');
    };
    this.get = function (id) {
        return $http.get('/districts/get/' + id).then(function (resp) {
            return resp.data;
        });
    }; 
    this.delete = function (id) {
        return $http.get('/districts/delete/' + id).then(function (resp) {
            return resp.data;
        });
    }; 
    this.getDistrictByStateId = function (id) {
        return $http.get('/districts/getDistrictByStateId/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.search = function (query) {
        return $http.post('/districts/search', query);
    };
    this.add = function (district) {
        return $http.post('/districts/add', district).then(function (resp) {
            return resp;
        });
    };
    this.edit = function (district) {
        return $http.post('/districts/edit/' + district.id, district).then(function (resp) {
            return resp;
        });
    };
    this.export = function (banner) {
        return $http.post('/districts/export', banner, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});