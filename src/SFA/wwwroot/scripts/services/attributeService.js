app.service('attributeService', function ($http) {
    this.getAll = function () {
        return $http.get('/attribute/all');
    };

    this.search = function (query) {
        return $http.post('/attribute/search', query);
    };

    this.get = function (id) {
        return $http.get('/attribute/get/' + id).then(function (resp) {
            return resp.data;
        });
    };

    this.getByAttribute = function (id) {
        return $http.get('/attribute/getByAttribute/' + id).then(function (resp) {
            return resp.data;
        });
    };

    this.getByTypeId = function (typeId) {
        return $http.get('/attribute/getByTypeId/' + typeId).then(function (resp) {
            return resp.data;
        });
    };

    //this.delete = function (id) {
    //    return $http.get('/countries/delete/' + id).then(function (resp) {
    //        return resp.data;
    //    });
    //};  

    this.add = function (attribute) {
        return $http.post('/attribute/add', attribute).then(function (resp) {
            return resp;
        });
    };

    this.edit = function (attribute) {
        return $http.post('/attribute/edit/' + attribute.id, attribute).then(function (resp) {
            return resp;
        });
    };

    this.saveAttribute = function (attribute) {
        return $http.post('/attribute/saveAttribute', attribute).then(function (resp) {
            return resp;
        });
    };

    this.import = function (file) {
        return $http.post('/attribute/import-attribute', file, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});