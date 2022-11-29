app.service('churchServiceTimeService', function ($http) {
    this.getAll = function () {
        return $http.get('/churchServiceTime/all');
    };
    this.get = function (id) {
        return $http.get('/churchServiceTime/get/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.search = function (query) {
        return $http.post('/churchServiceTime/search', query);
    };
    this.save = function (data) {
        return $http.post('/churchServiceTime/save', data).then(function (resp) {
            return resp;
        });
    };

    this.getTimeByChurch = function (churchId, day) {
        return $http.get('/churchServiceTime/getTimeByChurch/' + churchId + '/' + day).then(function (resp) {
            return resp.data;
        });
    };
});