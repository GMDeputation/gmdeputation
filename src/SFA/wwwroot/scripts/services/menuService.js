app.service('menuService', function ($http) {
    this.getAll = function () {
        return $http.get('/menu/api/all');
    };
    this.search = function (query) {
        return $http.post('/menu/api/search', query);
    };
    this.get = function (id) {
        return $http.get('/menu/api/get/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.save = function (menu) {
        return $http.post('/menu/api/save', menu).then(function (resp) {
            return resp;
        });
    };
});