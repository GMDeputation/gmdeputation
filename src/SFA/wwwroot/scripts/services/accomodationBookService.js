app.service('accomodationBookService', function ($http) {
    this.getAll = function () {
        return $http.get('/accomodation-booking/all');
    };

    this.search = function (query) {
        return $http.post('/accomodation-booking/search', query);
    };

    this.get = function (id) {
        return $http.get('/accomodation-booking/get/' + id).then(function (resp) {
            return resp.data;
        });
    };

    this.save = function (accommodation) {
        return $http.post('/accomodation-booking/save', accommodation).then(function (resp) {
            return resp;
        });
    };

    this.submit = function (accommodation) {
        return $http.post('/accomodation-booking/submit', accommodation).then(function (resp) {
            return resp;
        });
    };

    this.submitFeedBack = function (accommodation) {
        return $http.post('/accomodation-booking/submitFeedBack', accommodation).then(function (resp) {
            return resp;
        });
    };

    this.approved = function (accommodation) {
        return $http.post('/accomodation-booking/approved', accommodation).then(function (resp) {
            return resp;
        });
    };
});