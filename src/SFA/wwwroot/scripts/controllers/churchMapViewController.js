app.controller('churchMapViewController', function ($scope, churchService) {
    $scope.Markers = [];
    $scope.churches = [];
    function init() {

        churchService.getAll().then(function (resp) {
            $scope.churches = resp.data;

            if ($scope.churches.length > 0) {
                for (var i = 0; i < $scope.churches.length; i++) {
                    
                    if ($scope.churches[i].lat !== null && $scope.churches[i].lat !== undefined && $scope.churches[i].lon !== null && $scope.churches[i].lon !== undefined) {

                        $scope.Markers.push({
                            "title": $scope.churches[i].churchName,
                            "lat": $scope.churches[i].lat,
                            "lng": $scope.churches[i].lon,
                            "description": $scope.churches[i].address
                        });
                    }
                    }
            }
            else {
                $scope.Markers = [{}];
            }
            //Setting the Map options.
            $scope.MapOptions = {
                center: new google.maps.LatLng($scope.Markers[0].lat, $scope.Markers[0].lng),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.TERRAIN
                //mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            //Initializing the InfoWindow, Map and LatLngBounds objects.
            $scope.InfoWindow = new google.maps.InfoWindow();
            $scope.Latlngbounds = new google.maps.LatLngBounds();
            $scope.Map = new google.maps.Map(document.getElementById("dvMap"), $scope.MapOptions);

            //Looping through the Array and adding Markers.
            for (var j = 0; j < $scope.Markers.length; j++) {
                var data = $scope.Markers[j];
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
            }

            //Adjusting the Map for best display.
            $scope.Map.setCenter($scope.Latlngbounds.getCenter());
            $scope.Map.fitBounds($scope.Latlngbounds);
        });
    }
    init();
});