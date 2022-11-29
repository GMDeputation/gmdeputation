app.service('sectionService', function ($http) {
    this.getAll = function () {
        return $http.get('/sections/all');
    };
    this.get = function (id) {
        return $http.get('/sections/get/' + id).then(function (resp) {
            return resp.data;
        });
    }; 
    this.delete = function (id) {
        return $http.get('/sections/delete/' + id).then(function (resp) {
            return resp.data;
        });
    }; 
    this.getSectionByDistrictId = function (id) {
        return $http.get('/sections/GetSectionByDistrictId/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.search = function (query) {
        return $http.post('/sections/search', query);
    };
    this.add = function (section) {
        return $http.post('/sections/add', section).then(function (resp) {
            return resp;
        });
    };
    this.edit = function (section) {
        return $http.post('/sections/edit/' + section.id, section).then(function (resp) {
            return resp;
        });
    };
    this.export = function (file) {
        return $http.post('/sections/export-section', file, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };
});