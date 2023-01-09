app.service('churchService', function ($http) {
    this.getAll = function () {
        return $http.get('/churches/all');
    };
    this.getAllByUser = function () {
        return $http.get('/churches/getAllByUser');
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
    this.GetChurchByPastorID = function (id) {
        return $http.get('/churches/GetChurchByPastorID/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.getChurchByDistrict = function (id) {
        return $http.get('/churches/getChurchByDistrict/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.getChurchByDistrictAndMacroSchDtl = function (id, macroScheduleDetailId) {
        return $http.get('/churches/getChurchByDistrictAndMacroSchDtl/' + id + '/' + macroScheduleDetailId).then(function (resp) {
            return resp.data;
        });
    };
    this.getChurchByDistrictAndSection = function (distId, secId) {
        return $http.get('/churches/getChurchByDistrictAndSection/' + distId + '/' + secId).then(function (resp) {
            return resp.data;
        });
    };
    this.getChurchByDistrictAndSectionAndMacroSchDtl = function (distId, secId, macroScheduleDetailId) {
        return $http.get('/churches/getChurchByDistrictAndSectionAndMacroSchDtl/' + distId + '/' + secId + '/' + macroScheduleDetailId).then(function (resp) {
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