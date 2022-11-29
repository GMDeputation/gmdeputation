app.service('churchService', function ($http) {
    this.getAll = function () {
        return $http.get('/churches/all');
    };
    this.get = function (id) {
        return $http.get('/churches/get/' + id).then(function (resp) {
            return resp.data;
        });
    }; 
    this.delete = function (id) {
        return $http.get('/churches/delete/' + id).then(function (resp) {
            return resp.data;
        });
    }; 
    this.GetChurchBySectionId = function (id) {
        return $http.get('/churches/GetChurchBySectionId/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.getChurchByDistrict = function (id) {
        return $http.get('/churches/getChurchByDistrict/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.search = function (query) {
        return $http.post('/churches/search', query);
    };
    this.add = function (church) {
        return $http.post('/churches/add', church).then(function (resp) {
            return resp;
        });
    };
    this.edit = function (church) {
        return $http.post('/churches/edit/' + church.id, church).then(function (resp) {
            return resp;
        });
    };
    this.export = function (banner) {
        return $http.post('/churches/export', banner, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});