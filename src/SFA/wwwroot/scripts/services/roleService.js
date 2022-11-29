app.service('roleService', function ($http) {
    this.getAll = function () {
        var resp = $http.get('/role/api/all');
        return resp;
    };
    this.search = function (query) {
        return $http.post('/role/api/search', query);
    };
    this.get = function (id) {
        return $http.get('/role/api/get/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.save = function (role) {
        return $http.post('/role/api/save', role).then(function (resp) {
            return resp;
        });
    };
});