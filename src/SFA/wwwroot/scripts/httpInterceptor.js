'use strict';

//define(['app'], function(app) {
    app.config(['$httpProvider', function($httpProvider) {
        var injectParams = ['$q', '$rootScope', '$window'];
        var httpInterceptorGlobal = function ($q, $rootScope, $window) {
            var regexIsoUtc = /^(\d{4}|\+\d{6})(?:-(\d{2}))(?:-(\d{2}))(?:T(\d{2})):(\d{2}):(\d{2})Z$/;

            function matchDate(dateString) {
                if (dateString.length === 20) {
                    return dateString.match(regexIsoUtc);
                }
                return false;
            };

            function convertDateStringsToDates(object) {
                // ensure that we're processing an object
                if (typeof object !== "object") {
                    return object;
                }

                for (var key in object) {
                    if (!object.hasOwnProperty(key)) {
                        continue;
                    }
                    var value = object[key];

                    // check for string properties with a date format
                    if (typeof value === "string" && matchDate(value)) {
                        var date = new Date(value); // create the date from the date string
                        object[key] = moment(date).format("L"); // we're mutating the response directly
                    } else if (typeof value === "object") {
                        convertDateStringsToDates(value); // recurse into object
                    }
                }
                return null;
            }
            
            return {
                request: function(config) {
                    config.headers = config.headers || {};
                    //if ($window.sessionStorage.token) {
                    //    config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
                    //}
                    return config;
                },
                response: function (response) {
                    //if (response.data) {
                    //    convertDateStringsToDates(response.data);
                    //}
                    return response;
                },
                responseError: function(response) {
                    if (response.status === 401) {

                    }
                    return $q.reject(response);
                }
            };
        };

        httpInterceptorGlobal.$inject = injectParams;
        $httpProvider.interceptors.push(httpInterceptorGlobal);
    }]);
//});