app.service('serviceTypeService', function ($http) {
    this.getAll = function () {
        return $http.get('/serviceType/all');
    };
    this.get = function (id) {
        return $http.get('/serviceType/get/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.delete = function (id) {
        return $http.get('/serviceType/delete/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.search = function (query) {
        return $http.post('/serviceType/search', query);
    };
    this.save = function (data) {
        return $http.post('/serviceType/save', data).then(function (resp) {
            return resp;
        });
    };
    //this.export = function (banner) {
    //    return $http.post('/serviceType/export', banner, {
    //        transformRequest: angular.identity,
    //        headers: { 'Content-Type': undefined }
    //    }).then(function (resp) {
    //        return resp;
    //    });
    //};
});