app.service('menuGroupService', function ($http) {
    this.getAll = function () {
        return $http.get('/menu-group/api/all');
    };

    this.allCategory = function () {
        return $http.get('/menu-group/api/allCategory');
    };
    this.getByCategory = function (category) {
        return $http.get('/menu-group/api/getByCategory/' + category).then(function (resp) {
            return resp.data;
        });
    };
});