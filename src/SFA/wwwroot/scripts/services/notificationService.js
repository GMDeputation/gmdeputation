app.service('notificationService', function ($http) {
    this.getUnOpenedNotification = function () {
        return $http.get('/notification/getUnOpenedNotification');
    };
    this.openByUserId = function (id) {
        return $http.get('/notification/openByUserId/' + id).then(function (resp) {
            return resp.data;
        });
    };
});