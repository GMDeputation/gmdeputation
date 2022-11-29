app.controller('appointmentAddController', function ($scope, $filter, $window, $location, $mdDialog, appointmentService, churchService, churchServiceTimeService, macroScheduleService) {
    var macroScheduleDetailId = $location.absUrl().substr($location.absUrl().lastIndexOf('add/') + 4);

    $scope.backToList = function () {
        $window.location.href = '/appointments';
    };
    $scope.isShow = false;
    $scope.Markers = {};

    $scope.appointment = {
        eventTime: new Date()
    };

    $scope.message = {
        hour: 'Hour is required',
        minute: 'Minute is required',
        meridiem: 'Meridiem is required'
    };

    $scope.save = function () {
        $scope.time = $filter('date')($scope.appointment.eventTime, 'HH:mm:ss');
        $scope.appointment.eventTime = $scope.time;

        $scope.appointment.macroScheduleDetailId = macroScheduleDetailId;

        appointmentService.save($scope.appointment).then(processSuccess, processError);
    };

    $scope.searchChurch = function (searchText) {
        if ($scope.appointment.eventDate !== null && $scope.appointment.eventDate !== undefined) {
            $scope.test = [];

            angular.forEach($scope.churches, function (event) {

                if (event.churchName.toLowerCase().match(searchText.toLowerCase())) {
                    $scope.test.push(event);
                }

            });

            return $scope.test;
        } else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Select Appointment Date')
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            );

            return false;
        }
    };
    $scope.selectedChurchName = function ($item) {
        $scope.appointment.churchId = $item !== null && $item !== undefined ? $item.id : '0';

        if ($scope.appointment.churchId !== null && $scope.appointment.churchId !== undefined && $scope.appointment.churchId !== '0') {

            $scope.appointment.eventDate = new Date($scope.appointment.eventDate);
            var day = $scope.appointment.eventDate.getDay();

            angular.forEach($scope.days, function (detail) {
                if (detail.id === day) {
                    $scope.weekday = detail.name;
                }
            });

            churchServiceTimeService.getTimeByChurch($scope.appointment.churchId, $scope.weekday).then(function (resp) {
                $scope.times = resp;
            });

            ////********************Map****************//
            if ($item.lat !== null && $item.lat !== undefined && $item.lon !== null && $item.lon !== undefined) {
                $scope.isShow = true;

                $scope.Markers = { "title": $item.churchName, "lat": $item.lat, "lng": $item.lon, "description": $item.address }; 
                $scope.MapOptions = {
                    center: new google.maps.LatLng($scope.Markers.lat, $scope.Markers.lng),
                    zoom: 8,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                //Initializing the InfoWindow, Map and LatLngBounds objects.
                $scope.InfoWindow = new google.maps.InfoWindow();
                $scope.Latlngbounds = new google.maps.LatLngBounds();
                $scope.Map = new google.maps.Map(document.getElementById("dvMap"), $scope.MapOptions);

                var data = $scope.Markers;
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);

                //Initializing the Marker object.
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: $scope.Map,
                    title: data.title
                });

                //Adding InfoWindow to the Marker.
                (function (marker, data) {
                    google.maps.event.addListener(marker, "click", function (e) {
                        $scope.InfoWindow.setContent("<div style = 'width:200px;min-height:40px'>" + data.description + "</div>");
                        $scope.InfoWindow.open($scope.Map, marker);
                    });
                })(marker, data);

                //Plotting the Marker on the Map.
                $scope.Latlngbounds.extend(marker.position);

                //Adjusting the Map for best display.
                $scope.Map.setCenter($scope.Latlngbounds.getCenter());
                $scope.Map.fitBounds($scope.Latlngbounds);

            ////********************Map****************//
                var cities = [{
                    city: 'India',
                    desc: 'The Indian economy is the worlds seventh-largest by nominal GDP and third-largest by purchasing power parity (PPP).',
                    lat: 23.200000,
                    long: 79.225487
                }, {
                    city: 'New Delhi',
                    desc: 'Delhi, officially the National Capital Territory of Delhi, is the Capital territory of India. It has a population of about 11 million and a metropolitan population of about 16.3 million',
                    lat: 28.500000,
                    long: 77.250000
                }, {
                    city: 'Mumbai',
                    desc: 'Mumbai, formerly called Bombay, is a sprawling, densely populated city on India’s west coast',
                    lat: 19.000000,
                    long: 72.90000
                }, {
                    city: 'Kolkata',
                    desc: 'Kolkata is the capital of the Indian state of West Bengal. It is also the commercial capital of East India, located on the east bank of the Hooghly River',
                    lat: 22.500000,
                    long: 88.400000
                }, {
                    city: 'Chennai	',
                    desc: 'Chennai holds the colonial past and is an important city of South India. It was previously known as Madras',
                    lat: 13.000000,
                    long: 80.250000
                }, {
                    city: 'Gorakhpur',
                    desc: 'Gorakhpur also known as Gorakhshpur is a city along the banks of Rapti river in the eastern part of the state of Uttar Pradesh in India, near the Nepal border 273 east of the state capital Lucknow',
                    lat: 26.7588,
                    long: 83.3697
                }];

                //Create angular controller.
                var app = angular.module('googleAapApp', []);
                app.controller('googleAapCtrl', function ($scope) {
                    $scope.highlighters = [];
                    $scope.gMap = null;

                    var winInfo = new google.maps.InfoWindow();

                    var googleMapOption = {
                        zoom: 4,
                        center: new google.maps.LatLng(25, 80),
                        mapTypeId: google.maps.MapTypeId.TERRAIN
                    };

                    $scope.gMap = new google.maps.Map(document.getElementById('googleMap'), googleMapOption);



                    var createHighlighter = function (citi) {

                        var citiesInfo = new google.maps.Marker({
                            map: $scope.gMap,
                            position: new google.maps.LatLng(citi.lat, citi.long),
                            title: citi.city
                        });

                        citiesInfo.content = '<div>' + citi.desc + '</div>';

                        google.maps.event.addListener(citiesInfo, 'click', function () {
                            winInfo.setContent('<h1>' + citiesInfo.title + '</h1>' + citiesInfo.content);
                            winInfo.open($scope.gMap, citiesInfo);
                        });
                        $scope.highlighters.push(citiesInfo);
                    };

                    for (i = 0; i < cities.length; i++) {
                        createHighlighter(cities[i]);
                    }
                });
            }
            else {
                $scope.Markers = {};
                $scope.isShow = false;
            }
        }
    };

    $scope.dateChange = function () {
        $scope.church = null;
        $scope.times = [];
        $scope.appointment.eventTime = null;
    };

    function init() {
        $scope.appointment = [];     

        $scope.days = [{ "id": 0, "name": "Sunday" }, { "id": 1, "name": "Monday" }, { "id": 2, "name": "Tuesday" }, { "id": 3, "name": "Wednesday" },
        { "id": 4, "name": "Thurseday" }, { "id": 5, "name": "Friday" }, { "id": 6, "name": "Saturday" }];

        $scope.appointment = {};
        macroScheduleService.getMacroScheduleDetailsById(macroScheduleDetailId).then(function (resp) {
            $scope.macroScheduleDetails = resp;

            $scope.minDate = new Date($scope.macroScheduleDetails.startDate);
            $scope.maxDate = new Date($scope.macroScheduleDetails.endDate);

            churchService.getChurchByDistrict($scope.macroScheduleDetails.districtId).then(function (resp) {
                $scope.churches = resp;
            });
        });
    }
    init();
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Appointment saved successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/appointments';
            });
        }
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent(obj.data)
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            );
        }

    }
    function processError(error) {
        $mdDialog.show(
            $mdDialog.alert()
                .clickOutsideToClose(false)
                .title('Church Admin')
                .textContent('Failed To Save Appointment')
                .ok('OK')
        );
    }
});