app.controller('appointmentOfferingAddController', function ($scope, $filter, $window, $location, $mdDialog, appointmentOfferingService, churchService, churchServiceTimeService, macroScheduleService, userService, districtService) {
    var macroScheduleDetailId = $location.absUrl().substr($location.absUrl().lastIndexOf('addOfferingOnly') + 16);

    $scope.backToList = function () {
        $window.location.href = '/appointments';
    };
    $scope.isShow = false;
    $scope.Markers = [];
    $scope.churches = [];

    //$scope.appointment = {
    //    eventTime: new Date()
    //};
    $scope.pastors = {
        fullName: "",
        id: ""
    }
    $scope.church = {
        fullName: "",
        id: "",
        city: "",
        Districtid: ""
    }

    $scope.districtValues = {
        name: ""

    }
    $scope.message = {
        hour: 'Hour is required',
        minute: 'Minute is required',
        meridiem: 'Meridiem is required'
    };
    $scope.searchPastors = function (searchText) {

        $scope.test = [];

        angular.forEach($scope.pastors, function (event) {

            if (event.pastorName.toLowerCase().match(searchText.toLowerCase())) {
                $scope.test.push(event);
            }



        });
        return $scope.test;
    };
    //This is grabbing the pastor selected and wil default the values for the rest of the fields in the form
    $scope.pastorSelected = function ($item,index) {

        //Getting all the churches that are tied to that pastor
        churchService.GetChurchByPastorID($item).then(function (resp) {
            $scope.appointments[index].church = resp;
            //$scope.church = resp;
        });
        $scope.appointments[index].pastorId = $item;

        //$scope.ChurchSelected();
    };

    //This is grabbing the District of the Church that was selected
    $scope.ChurchSelected = function ($item, index) {

        //Getting the distritc that is tied to that church
        districtService.get($item).then(function (resp) {
            $scope.appointments[index].districtValues = resp;
            //$scope.districtValues = resp;
        });

        //$scope.selectedChurchName($item, index);
    };


    $scope.searchPastors = function (searchText) {

        $scope.test = [];

        angular.forEach($scope.pastors, function (event) {

            if (event.pastorName.toLowerCase().match(searchText.toLowerCase())) {
                $scope.test.push(event);
            }



        });
        return $scope.test;
    };
    $scope.save = function () {
        //$scope.time = $filter('date')($scope.appointment.eventTime, 'HH:mm:ss');
        //$scope.appointment.eventTime = $scope.time;
        //$scope.appointment.macroScheduleDetailId = macroScheduleDetailId;

        angular.forEach($scope.appointments, function (appoint) {

            $scope.time = $filter('date')(appoint.eventTime, 'HH:mm:ss');
            appoint.eventTime = $scope.time;

            angular.forEach(appoint.times, function (time) {
                if ($scope.time == time.serviceTime) {
                    $scope.serviceTypeID = time.id;
                }
            });

            appoint.serviceTypeID = $scope.serviceTypeID;
            appoint.macroScheduleDetailId = macroScheduleDetailId;
            appoint.offeringOnly = true;
        });
             
        appointmentOfferingService.add($scope.appointments).then(processSuccess, processError);
    };

    $scope.searchChurch = function (searchText, index) {
        if (true) {
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
    $scope.selectedChurchName = function ($item, index) {
        $scope.appointments[index].churchId = $item !== null && $item !== undefined ? $item[index].id : 0;

        if ($scope.appointments[index].churchId !== null && $scope.appointments[index].churchId !== undefined && $scope.appointments[index].churchId !== 0) {

            $scope.getServiceTime(index);
        }
    };

    $scope.addNewRow = function () {
        $scope.appointments = $scope.appointments.concat({ "pastor": null, "churchId": null, "districtID": null, "eventDate": null, "eventTime": null, "description": null, "macroScheduleDetailId": null, "times": [] });

        userService.GetAllPastorsByDistrict($scope.macroScheduleDetails.districtId).then(function (resp) {
            var index = Object.keys($scope.appointments).length - 1;
            $scope.appointments[index].pastors = resp;
        });

    };
    $scope.deleteRow = function (index) {
        var confirm = $mdDialog.confirm()
            .title('Church Admin')
            .textContent('Are You sure To Delete?')
            .ariaLabel('Alert Dialog')
            .cancel('No')
            .ok('Yes');

        $mdDialog.show(confirm).then(function () {
            $scope.appointments.splice(index, 1);
        }, function () {
            $mdDialog.hide();
        });
    };

    $scope.dateChange = function (index) {
        $scope.appointments[index].times = [
        { serviceTime:'09:00:00', timeString: '9AM:Breakfast' }
        , {
            serviceTime: '13:00:00' ,timeString: '1PM:Lunch' }
            ,
        { serviceTime: '18:00:00', timeString: '6PM: Dinner' } 

        ];
        $scope.appointments[index].eventTime = null;


        $scope.appointments[index].eventDate = new Date($scope.appointments[index].eventDate);
        var day = $scope.appointments[index].eventDate.getDay();

        angular.forEach($scope.days, function (detail) {
            if (detail.id === day) {
                $scope.weekday = detail.name;
            }
       });

       $scope.selectedChurchName($scope.appointments[index].church, index);

        if ($scope.appointments[index].churchId !== null && $scope.appointments[index].churchId !== undefined && $scope.appointments[index].churchId !== '0') {
           $scope.getServiceTime(index);
        }

        $scope.loadMap();
    };

    $scope.getServiceTime = function (index) {
        $scope.appointments[index].eventDate = new Date($scope.appointments[index].eventDate);
        var day = $scope.appointments[index].eventDate.getDay();

        angular.forEach($scope.days, function (detail) {
            if (detail.id === day) {
                $scope.weekday = detail.name;
            }
        });

        churchServiceTimeService.getTimeByChurch($scope.appointments[index].churchId, $scope.weekday).then(function (resp) {
            //$scope.appointments[index].times = resp;
        });
    };


    $scope.loadMap = function () {
        if ($scope.churches.length > 0) {
            $scope.isShow = true;

            for (var i = 0; i < $scope.churches.length; i++) {

                if ($scope.churches[i].lat !== null && $scope.churches[i].lat !== undefined && $scope.churches[i].lon !== null && $scope.churches[i].lon !== undefined) {

                    $scope.Markers.push({
                        "title": $scope.churches[i].churchName,
                        "lat": $scope.churches[i].lat,
                        "lng": $scope.churches[i].lon,
                        "description": $scope.churches[i].mailAddress,
                        "section": $scope.churches[i].sectionName,
                        "district": $scope.churches[i].districtName,
                        "id": $scope.churches[i].id,
                        "pastor": $scope.churches[i].pastor,
                        "serviceTypewiseTime": $scope.churches[i].serviceTypewiseTime,
                        "churchServiceTimes": $scope.churches[i].churchServiceTimes
                    });
                }
            }
        }
        else {
            $scope.Markers = [];
            $scope.isShow = false;
        }
        //Setting the Map options.
        $scope.MapOptions = {
            center: new google.maps.LatLng($scope.Markers[0].lat, $scope.Markers[0].lng),
            zoom: 10,
            mapTypeId: google.maps.MapTypeId.TERRAIN
        };

        //Initializing the InfoWindow, Map and LatLngBounds objects.
        $scope.InfoWindow = new google.maps.InfoWindow();
        $scope.Latlngbounds = new google.maps.LatLngBounds();
        $scope.Map = new google.maps.Map(document.getElementById("dvMap1"), $scope.MapOptions);

        //Looping through the Array and adding Markers.
        for (var i = 0; i < $scope.Markers.length; i++) {
            var data = $scope.Markers[i];
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);

            //Initializing the Marker object.
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: $scope.Map,
                title: data.title
            });

            //Setting the Icons to Green.
            angular.forEach(data, function (time) {
                    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png')
                
            });

            //Adding InfoWindow to the Marker.
            (function (marker, data) {
                google.maps.event.addListener(marker, "click", function (e) {
                    // $scope.Map.setZoom(8);

                    //$scope.InfoWindow.setContent("<div onclick='setChurch(\""+data.id+"\",\""+data.title+"\")'><div style = 'width:200px'>" + data.title + "</div>" + "<div>" + data.description + "</div>" + "<div>" + data.section + "</div>" + "<div>" + data.district + "</div></div>");
                    $scope.InfoWindow.setContent("<div style = 'color:blue'><div style = 'width:200px'>" + data.title + "</div>" + "<div>" + data.description + "</div>"
                        + "<div>Section:" + data.section + "</div>" + "<div>District:" + data.district + "</div>" + "<div>" + "Pastor : " + data.pastor +
                        "</div>" + "<div style = 'color:purple'>" + "Service Time : " + data.serviceTypewiseTime + "</div></div>");
                    $scope.Map.setZoom(8);
                    $scope.InfoWindow.open($scope.Map, marker);
                    var length = $scope.appointments.length;
                    length = length - 1;

                        $scope.appointments[length].pastor =  data.pastor;
                        $scope.appointments[length].churchId = data.id;
                        $scope.appointments[length].churchItem = { "id": data.id, "churchName": data.title };

                       // $scope.getServiceTime(length);
                  
                });
            })(marker, data);

            //Plotting the Marker on the Map.
            $scope.Latlngbounds.extend(marker.position);
        }

        //Adjusting the Map for best display.
        $scope.Map.setCenter($scope.Latlngbounds.getCenter());
        $scope.Map.fitBounds($scope.Latlngbounds);
    };


    function init() {
        $scope.appointments = [{ "pastor": null, "churchId": null, "districtID": null, "eventDate": null, "eventTime": null, "description": null, "macroScheduleDetailId": null, "times": [] }];

        $scope.days = [{ "id": 0, "name": "Sunday" }, { "id": 1, "name": "Monday" }, { "id": 2, "name": "Tuesday" }, { "id": 3, "name": "Wednesday" },
        { "id": 4, "name": "Thursday" }, { "id": 5, "name": "Friday" }, { "id": 6, "name": "Saturday" }];

        //$scope.appointment = {};
        macroScheduleService.getMacroScheduleDetailsById(macroScheduleDetailId).then(function (resp) {
            $scope.macroScheduleDetails = resp;

            $scope.minDate = new Date($scope.macroScheduleDetails.startDate);
            $scope.maxDate = new Date($scope.macroScheduleDetails.endDate);

            userService.GetAllPastorsByDistrict($scope.macroScheduleDetails.districtId).then(function (resp) {

                $scope.appointments[0].pastors = resp;
            });


            churchService.getChurchByDistrictAndMacroSchDtl($scope.macroScheduleDetails.districtId, macroScheduleDetailId).then(function (resp) {
                $scope.churches = resp;

                $scope.loadMap();
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