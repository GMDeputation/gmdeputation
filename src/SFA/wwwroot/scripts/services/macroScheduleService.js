app.service('macroScheduleService', function ($http) {
    this.getAll = function () {
        return $http.get('/macroSchedule/all');
    };
    this.getAllapproved = function (query) {
        return $http.post('/macroSchedule/getAllapproved', query);
    };
    this.get = function (id) {
        return $http.get('/macroSchedule/get/' + id).then(function (resp) {
            return resp.data;
        });
    };
    this.search = function (query) {
        var tmp = query.data;
        return $http.post('/macroSchedule/search', query);
    };
    this.save = function (data) {
        return $http.post('/macroSchedule/save', data).then(function (resp) {
            return resp;
        });
    };

    this.getMacroScheduleDetailsById = function (id) {
        return $http.get('/macroSchedule/getMacroScheduleDetailsById/' + id).then(function (resp) {
            return resp.data;
        });
    };

    this.edit = function (data) {
        return $http.post('/macroSchedule/edit', data).then(function (resp) {
            return resp;
        });
    };

    this.approved = function (data) {
        return $http.post('/macroSchedule/approved', data).then(function (resp) {
            return resp;
        });
    };

    this.rejected = function (data) {
        return $http.post('/macroSchedule/rejected', data).then(function (resp) {
            return resp;
        });
    };

    this.approvedMacroSchedulesIds = function (data) {
        return $http.post('/macroSchedule/approvedMacroSchedulesIds', data).then(function (resp) {
            return resp;
        });
    };

    this.rejectMacroSchedulesIds = function (data) {
        return $http.post('/macroSchedule/rejectMacroSchedulesIds', data).then(function (resp) {
            return resp;
        });
    };
    this.importData = function (file) {
        return $http.post('/macroSchedule/importData', file, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (resp) {
            return resp;
        });
    };

    this.cancel = async function (data) {
        return await $http.post('/macroSchedule/cancel', data).then(function (resp) {
            return resp.data;
        });
    };
});