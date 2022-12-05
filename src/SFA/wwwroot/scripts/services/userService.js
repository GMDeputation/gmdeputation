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

    this.exportListData = function () {
        return $http.get('/user/exportListData', {
            responseType: 'arraybuffer'
        }).then(function (resp) {
            headers = resp.headers();
            var filename = headers['x-filename'];
            var contentType = headers['content-type'];

            var linkElement = document.createElement('a');
            try {
                var blob = new Blob([resp.data], { type: contentType });
                var url = window.URL.createObjectURL(blob);

                linkElement.setAttribute('href', url);
                linkElement.setAttribute("download", filename);

                var clickEvent = new MouseEvent("click", {
                    "view": window,
                    "bubbles": true,
                    "cancelable": false
                });
                linkElement.dispatchEvent(clickEvent);
            } catch (ex) {
                console.log(ex);
            }
        });
    };

    this.updateDistrictAndSection = function (users) {
        return $http.post('/user/api/updateDistrictAndSection', users).then(function (resp) {
            return resp;
        });
    };
});