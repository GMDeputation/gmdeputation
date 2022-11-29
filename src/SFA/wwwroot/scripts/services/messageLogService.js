app.service('messageLogService', function ($http) {
    this.getAllUnopened = function () {
        return $http.get('/api/message-logs/unopened');
    };
});