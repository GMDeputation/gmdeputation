app.service('reportsService', function ($http) {

    this.missionaryServiceRerport = function (reportParams) {
        return $http.post('/reports/getMissionaryServiceReportData', reportParams);
    };

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

    this.generateUserActivityReportWord = function (datas) {
        return $http.post('/reports/userActivityReportWord', datas, {
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


    this.generateChurchServiceCountReportPdf = function (datas) {
        return $http.post('/reports/generateChurchServiceCountReportPdf', datas, {
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


    this.generateChurchServiceCountReportWord = function (datas) {
        return $http.post('/reports/generateChurchServiceCountReportWord', datas, {
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

    this.generatePastorContactReportPdf = function (datas) {
        return $http.post('/reports/generatePastorContactReportPdf', datas, {
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


    this.generatePastorContactReportWord = function (datas) {
        return $http.post('/reports/generatePastorContactReportWord', datas, {
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

    this.generateMacroscheduleWiseAppoinmentReportPdf = function (datas) {
        return $http.post('/reports/generateMacroscheduleWiseAppoinmentReportPdf', datas, {
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

    this.generateMacroscheduleWiseAppoinmentReportWord = function (datas) {
        return $http.post('/reports/generateMacroscheduleWiseAppoinmentReportWord', datas, {
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

    this.generateChurchWiseAppoinmentReportPdf = function (datas) {
        return $http.post('/reports/generateChurchWiseAppoinmentReportPdf', datas, {
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


    this.generateChurchWiseAppoinmentReportWord = function (datas) {
        return $http.post('/reports/generateChurchWiseAppoinmentReportWord', datas, {
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

    this.generateOfferingOnlyReportPdf = function (datas) {
        return $http.post('/reports/generateOfferingOnlyReportPdf', datas, {
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

    this.generateOfferingOnlyReportWord = function (datas) {
        return $http.post('/reports/generateOfferingOnlyReportWord', datas, {
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

    this.generateMissionaryScheduleReportPdf = function (datas) {
        return $http.post('/reports/generateMissionaryScheduleReportPdf', datas, {
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

    this.generateMissionaryScheduleReportWord = function (datas) {
        return $http.post('/reports/generateMissionaryScheduleReportWord', datas, {
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
    this.generatePastorAppoinmentReportPdf = function (datas) {
        return $http.post('/reports/generatePastorAppoinmentReportPdf', datas, {
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
    this.generatePastorAppoinmentReportWord = function (datas) {
        return $http.post('/reports/generatePastorAppoinmentReportWord', datas, {
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

    this.generateAccomodationBookingReportPdf = function (datas) {
        return $http.post('/reports/generateAccomodationBookingReportPdf', datas, {
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

    this.generateAccomodationBookingReportWord = function (datas) {
        return $http.post('/reports/generateAccomodationBookingReportWord', datas, {
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