app.service('reportsService', function ($http) {

    this.getUserActivityData = function (reportParams) {
        return $http.post('/reports/getUserActivityData', reportParams);
    };

    this.generateUserActivityReportPdf = function (datas) {
        return $http.post('/reports/userActivityReportPdf', datas, {
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

    this.generateUserActivityReport = function (datas) {
        return $http.post('/reports/userActivityReport', datas, {
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



    this.getChurchServiceCountReportData = function (reportParams) {
        return $http.post('/reports/getChurchServiceCountReportData', reportParams);
    };

    this.generateChurchServiceCountReport = function (datas) {
        return $http.post('/reports/generateChurchServiceCountReport', datas, {
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

    this.getPastorContactData = function (reportParams) {
        return $http.post('/reports/getPastorContactData', reportParams);
    };

    this.generatePastorContactReport = function (datas) {
        return $http.post('/reports/generatePastorContactReport', datas, {
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

    this.getMacroscheduleWiseAppoinmentData = function (reportParams) {
        return $http.post('/reports/getMacroscheduleWiseAppoinmentData', reportParams);
    };

    this.generateMacroscheduleWiseAppoinmentReport = function (datas) {
        return $http.post('/reports/generateMacroscheduleWiseAppoinmentReport', datas, {
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

    this.getChurchWiseAppoinmentData = function (reportParams) {
        return $http.post('/reports/getChurchWiseAppoinmentData', reportParams);
    };

    this.generateChurchWiseAppoinmentReport = function (datas) {
        return $http.post('/reports/generateChurchWiseAppoinmentReport', datas, {
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

    this.getOfferingOnlyReportData = function (reportParams) {
        return $http.post('/reports/getOfferingOnlyReportData', reportParams);
    };

    this.generateOfferingOnlyReport = function (datas) {
        return $http.post('/reports/generateOfferingOnlyReport', datas, {
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

    this.getMissionaryScheduleData = function (reportParams) {
        return $http.post('/reports/getMissionaryScheduleData', reportParams);
    };

    this.generateMissionaryScheduleReport = function (datas) {
        return $http.post('/reports/generateMissionaryScheduleReport', datas, {
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

    this.getPastorAppoinmentData = function (reportParams) {
        return $http.post('/reports/getPastorAppoinmentData', reportParams);
    };

    this.generatePastorAppoinmentReport = function (datas) {
        return $http.post('/reports/generatePastorAppoinmentReport', datas, {
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

    this.getAccomodationBookingReportData = function (reportParams) {
        return $http.post('/reports/getAccomodationBookingReportData', reportParams);
    };

    this.generateAccomodationBookingReport = function (datas) {
        return $http.post('/reports/generateAccomodationBookingReport', datas, {
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
});