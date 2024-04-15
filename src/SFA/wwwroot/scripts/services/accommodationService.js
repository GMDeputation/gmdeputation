app.service('accommodationService', function ($http) {
    this.getAll = function () {
        return $http.get('/accommodations/all');
    };

    this.search = function (query) {
        return $http.post('/accommodations/search', query);
    };

    this.get = function (id) {
        return $http.get('/accomodation-booking/get/' + id).then(function (resp) {
            return resp.data;
        });
    };

    this.save = function (accommodation) {
        return $http.post('/accommodations/save', accommodation).then(function (resp) {
            return resp;
        });
    };

    this.import = function (file) {
        return $http.post('/accommodations/import-accommodation', file, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});