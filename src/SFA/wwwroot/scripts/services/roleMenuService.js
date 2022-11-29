app.service('roleMenuService', function ($http) {
    this.getMenuByRole = function (roleId) {
        return $http.get('/roleMenu/api/getMenuByRole/' + roleId).then(function (resp) {
            return resp.data;
        });
    };

    this.saveRoleMenu = function (roleMenu) {
        return $http.post('/roleMenu/api/saveRoleMenu', roleMenu).then(function (resp) {
            return resp;
        });
    };
});