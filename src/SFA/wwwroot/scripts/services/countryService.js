app.service('countryService', function ($http) {
    this.getAll = function () {
        return $http.get('/countries/all');
    };
    this.get = function (id) {
        return $http.get('/countries/get/' + id).then(function (resp) {
            return resp.data;
        });
    };   
    this.delete = function (id) {
        return $http.get('/countries/delete/' + id).then(function (resp) {
            return resp.data;
        });
    };  
    this.search = function (query) {
        return $http.post('/countries/search', query);
    };
    this.add = function (country) {
        return $http.post('/countries/add', country).then(function (resp) {
            return resp;
        });
    };
    this.edit = function (country) {
        return $http.post('/countries/edit/' + country.id, country).then(function (resp) {
            return resp;
        });
    };
    this.export = function (banner) {
        return $http.post('/countries/export', banner, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});