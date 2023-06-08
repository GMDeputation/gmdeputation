app.controller('churchEditController', function ($scope, $window, $location, $mdDialog, churchService, sectionService, districtService) {
    var id = $location.absUrl().substr($location.absUrl().lastIndexOf('edit/') + 5);


    //This is to autocomplete the address 
    var pickup = document.getElementById('Address');

    var options = {
        componentRestrictions: {
            country: 'us'
        }
    };
    var autocompletePickup = new google.maps.places.Autocomplete(pickup, options);

    (function (scope) {
        google.maps.event.addListener(autocompletePickup, 'place_changed', function () {
            var place = autocompletePickup.getPlace();
            scope.church.address = place.formatted_address;
        });
    })($scope);

    //This is to autocomplete the Mail Address
    var pickup2 = document.getElementById('MailAddress');

    var autocompletePickup2 = new google.maps.places.Autocomplete(pickup2, options);

    (function (scope) {
        google.maps.event.addListener(autocompletePickup2, 'place_changed', function () {
            var place = autocompletePickup2.getPlace();
            scope.church.mailAddress = place.formatted_address;
        });
    })($scope);



    const isNumericInput = (event) => {
        const key = event.keyCode;
        return ((key >= 48 && key <= 57) || // Allow number line
            (key >= 96 && key <= 105) // Allow number pad
        );
    };

    const isModifierKey = (event) => {
        const key = event.keyCode;
        return (event.shiftKey === true || key === 35 || key === 36) || // Allow Shift, Home, End
            (key === 8 || key === 9 || key === 13 || key === 46) || // Allow Backspace, Tab, Enter, Delete
            (key > 36 && key < 41) || // Allow left, up, right, down
            (
                // Allow Ctrl/Command + A,C,V,X,Z
                (event.ctrlKey === true || event.metaKey === true) &&
                (key === 65 || key === 67 || key === 86 || key === 88 || key === 90)
            )
    };

    const enforceFormat = (event) => {
        // Input must be of a valid number format or a modifier key, and not longer than ten digits
        if (!isNumericInput(event) && !isModifierKey(event)) {
            event.preventDefault();
        }
    };

    const formatToPhone = (event) => {
        if (isModifierKey(event)) { return; }

        const input = event.target.value.replace(/\D/g, '').substring(0, 10); // First ten digits of input only
        const areaCode = input.substring(0, 3);
        const middle = input.substring(3, 6);
        const last = input.substring(6, 10);

        if (input.length > 6) { event.target.value = `(${areaCode}) ${middle} - ${last}`; }
        else if (input.length > 3) { event.target.value = `(${areaCode}) ${middle}`; }
        else if (input.length > 0) { event.target.value = `(${areaCode}`; }
    };


    //These are the liusteners to auto format the phone numbers when a user is adding a new user
    const inputElement = document.getElementById('phone');
    inputElement.addEventListener('keydown', enforceFormat);
    inputElement.addEventListener('keyup', formatToPhone);

    const inputElement2 = document.getElementById('phone2');
    inputElement2.addEventListener('keydown', enforceFormat);
    inputElement2.addEventListener('keyup', formatToPhone);


    $scope.backToList = function () {
        $window.location.href = '/churches';
    };
    $scope.save = function () {
        churchService.edit($scope.church).then(processSuccess, processError);
    };

    $scope.searchDistrict = function (searchText) {
        $scope.district = [];

        if (searchText !== null && searchText !== undefined && searchText !== "") {
            searchText = searchText.toLowerCase();
        }

        angular.forEach($scope.districts, function (event) {
            if (event.name.toLowerCase().match(searchText)) {
                $scope.district.push(event);
            }
        });

        return $scope.district;
    };
    $scope.selectedDistrictName = function ($item) {
        $scope.church.districtId = $item !== null && $item !== undefined ? $item.id : '0';
        $scope.sections = [];

        if ($scope.church.districtId !== null && $scope.church.districtId !== undefined && $scope.church.districtId !== '0') {

            sectionService.getSectionByDistrictId($scope.church.districtId).then(function (resp) {
                $scope.sections = resp;
            });
        }
    };

    function init() {
        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });

        churchService.get(id).then(function (data) {
            $scope.church = data;
            $scope.church.sectionId = data.sectionId;

            $scope.district = { "id": $scope.church.districtId, "name": $scope.church.districtName };
        });        
        //sectionService.getAll().then(function (resp) {
        //    $scope.sections = resp.data;
        //});
    }
    function processSuccess(obj) {
        if (obj.data === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent("Church updated successfully")
                    .ariaLabel('Alert Dialog')
                    .ok('OK')
            ).then(function () {
                $window.location.href = '/churches';
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
                .textContent('Failed To Update Church')
                .ok('OK')
        );
    }

    init();
});