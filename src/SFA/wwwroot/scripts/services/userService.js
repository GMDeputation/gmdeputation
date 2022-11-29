app.service('userService', function ($http) {
    this.getAll = function () {
        return $http.get('/user/api/all');
    };
    this.changePass = function (user) {
        return $http.post('/user/changePass', user);
    };
    this.search = function (query) {
        return $http.post('/user/api/search', query);
    };
    this.get = function (id) {
        return $http.get('/user/api/get/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.getAllMissionariesUser = function () {
        return $http.get('/user/api/getAllMissionariesUser').then(function (resp) {
            return resp.data;
        });
    };
    this.save = function (user) {
        return $http.post('/user/api/save', user).then(function (resp) {
            return resp;
        });
    };

    this.export = function (file) {
        return $http.post('/user/export-section', file, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});