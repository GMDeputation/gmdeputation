app.service('stateService', function ($http) {
    this.getAll = function () {
        return $http.get('/states/all');
    };
    this.get = function (id) {
        return $http.get('/states/get/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.delete = function (id) {
        return $http.get('/states/delete/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.getStateByCountryId = function (id) {
        return $http.get('/states/getStateByCountryId/' + id).then(function (resp) {
            return resp.data;
        });
    }; 
    this.search = function (query) {
        return $http.post('/states/search', query);
    };
    this.add = function (state) {
        return $http.post('/states/add', state).then(function (resp) {
            return resp;
        });
    };
    this.edit = function (state) {
        return $http.post('/states/edit/' + state.id, state).then(function (resp) {
            return resp;
        });
    };
    this.export = function (banner) {
        return $http.post('/states/export', banner, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});