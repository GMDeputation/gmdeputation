app.service('commonValueService', function ($http) {
    this.getAllGenders = function () {
        return $http.get('/api/common-values/genders');
    };
    this.getAllReligions = function () {
        return $http.get('/api/common-values/religions');
    };
    this.getAllMaritalStatuses = function () {
        return $http.get('/api/common-values/marital-statuses');
    };
    this.getAllCastes = function () {
        return $http.get('/api/common-values/castes');
    };
    this.getAllBloodGroups = function () {
        return $http.get('/api/common-values/blood-groups');
    };
});